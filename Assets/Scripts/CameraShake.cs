using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
   //С помощью данной функции будет запущена вибрация извне данного класса
public void ShakeCamera(float duration, float magnitude, float noize)
{
    //Запускаем корутину вибрации
    StartCoroutine(ShakeCameraCor(duration, magnitude, noize));
}

//Преимущество корутин в данной реализации очевидно
//Если реализовыывать классически образом, используя функцию Update
//Слишком много полей пришлось бы сохранять в полях класса
private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
{
    //Инициализируем счётчиков прошедшего времени
    float elapsed = 0f;
    //Сохраняем стартовую локальную позицию
     startPosition = transform.localPosition;
    //Генерируем две точки на "текстуре" шума Перлина
    Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
    Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

    //Выполняем код до тех пор пока не иссякнет время
    while (elapsed < duration)
    {
        //Генерируем две очередные координаты на текстуре Перлина в зависимости от прошедшего времени
        Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
        Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
        //Создаём новую дельту для камеры и умножаем её на длину дабы учесть желаемый разброс
        Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
        cameraPostionDelta *= magnitude;

        //Перемещаем камеру в нувую координату
        transform.localPosition = startPosition + (Vector3)cameraPostionDelta;

        //Увеличиваем счётчик прошедшего времени
        elapsed += Time.deltaTime;
        //Приостанавливаем выполнение корутины, в следующем кадре она продолжит выполнение с данной точки
        yield return null;
    }
	//По завершении вибрации, возвращаем камеру в исходную позицию
    transform.localPosition = startPosition;
    
}
public void ShakeRotateCamera(float duration, float angleDeg, Vector2 direction)
{
    //Запускаем корутину вращения камеры
    StartCoroutine(ShakeRotateCor(duration, angleDeg, direction));
}


private IEnumerator ShakeRotateCor(float duration, float angleDeg, Vector2 direction)
{
    //Счетчик прошедшего времени
    float elapsed = 0f;
    //Запоминаем начальное вращение камеры по аналогии с вибрацией камеры
    Quaternion startRotation = transform.localRotation;

    //Для удобства добавляем переменную середину нашего таймера
    //Ибо сначала отклонение будет идти на увеличение, а затем на уменьшение
    float halfDuration = duration / 2;
    //Приводим направляющий вектор к единичному вектору, дабы не портить вычисления
    direction = direction.normalized;
    while (elapsed < duration)
    {
        //Сохраняем текущее направление ибо мы будем менять данный вектор
        Vector2 currentDirection = direction;
        //Подсчёт процентного коэффициента для функции Lerp[0..1]
        //До середины таймера процент увеличивается, затем уменьшается
        float t = elapsed < halfDuration ? elapsed / halfDuration : (duration - elapsed) / halfDuration;
        //Текущий угол отклонения
        float currentAngle = Mathf.Lerp(0f, angleDeg, t);
        //Вычисляем длину направляющего вектора из тангенса угла.
        //Недостатком данного решения будет являться то
        //Что угол отклонения должен находится в следующем диапазоне (0..90)
        currentDirection *= Mathf.Tan(currentAngle * Mathf.Deg2Rad);
        //Сумма векторов - получаем направление взгляда на текущей итерации
        Vector3 resDirection = ((Vector3)currentDirection + Vector3.forward).normalized;
        //С помощью Quaternion.FromToRotation получаем новое вращение
        //Изменяем локальное вращение, дабы во время вращения, если игрок будет управлять камерой
        //Все работало корректно
        transform.localRotation = Quaternion.FromToRotation(Vector3.forward, resDirection);

        elapsed += Time.deltaTime;
        yield return null;
    }
    //Восстанавливаем вращение
    transform.localRotation = startRotation;
}
}

