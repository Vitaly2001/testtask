using System;

public class Zakazi // класс заказов
{ 
    public string orderName= " "; // позже зададим имя для заказа чтобы при выводе в консоль было легче понять какой курьер к какому заказу прикреплен
    public float[] otkuda = new float[2]; // массив из 2 яйчеек для записи туда координат
    public float[] kuda = new float[2];
    public int price = 0; // предлагаемая цена
    public Kuryeri optimalCourier = new Kuryeri(); // сюда присвоится оптимальный курьер дял конкретного заказа
}

public class Kuryeri //класс курьеров
{ 
    public string courierName = " "; // тоже самое что и для заказа
    public float[] kuryerpos = new float[2];
}

public class Mainprogramm
{

    public static float CalculateDistance(float[] point1, float[] point2) // метод для расчета расстояния от заказа до курьера (расчет дистанции 2 точек)
    {
        float deltaX = point2[0] - point1[0];
        float deltaY = point2[1] - point1[1];
        return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    } // понимаю что такой подход работает только в этих придуманных условиях пустого 2D поля с точками без препятствий, 
    //в реальности практичнее было бы применение алгоритма А* для нахождения лучшего пути в условиях пробок и прочих препятствий.

    public static void Main()
    {
        Zakazi[] orders = new Zakazi[3]; // массив для хранения в нем заказов

        // Не успеваю реализовать ввод данных с терминала либо другим способом поэтому задал параметры для заказов и курьеров руками, чтобы просто затестить алгоритм.
        orders[0] = new Zakazi();
        orders[0].orderName = "Order 1"; // имя заказа 
        orders[0].otkuda[0] = 32.231f; // условно координата X
        orders[0].otkuda[1] = 231f; // Y
        orders[0].kuda[0] = 12.424f; // X
        orders[0].kuda[1] = 2.234f; // Y
        orders[0].price = 250; // цена


        orders[1] = new Zakazi();
        orders[1].orderName = "Order 2";
        orders[1].otkuda[0] = 73.9643f;
        orders[1].otkuda[1] = 223.1245f;
        orders[1].kuda[0] = 33.123f;
        orders[1].kuda[1] = 1.2f;
        orders[1].price = 300;

        orders[2] = new Zakazi();
        orders[2].orderName = "Order 3";
        orders[2].otkuda[0] = 74.9643f;
        orders[2].otkuda[1] = 10.1245f;
        orders[2].kuda[0] = 53.123f;
        orders[2].kuda[1] = 42.2f; 
        orders[2].price = 150;

        Kuryeri[] couriers = new Kuryeri[4]; // аналогично с заказами

        couriers[0] = new Kuryeri();
        couriers[0].courierName = "Courier 1"; 
        couriers[0].kuryerpos[0] = 11.2342f;
        couriers[0].kuryerpos[1] = 42.537f;

        couriers[1] = new Kuryeri();
        couriers[1].courierName = "Courier 2";
        couriers[1].kuryerpos[0] = 124.244642f;
        couriers[1].kuryerpos[1] = 21.5327f;

        couriers[2] = new Kuryeri();
        couriers[2].courierName = "Courier 3";
        couriers[2].kuryerpos[0] = 94.2342f;
        couriers[2].kuryerpos[1] = 12.100f;

        couriers[3] = new Kuryeri();
        couriers[3].courierName = "Courier 4";
        couriers[3].kuryerpos[0] = 74.9842f;
        couriers[3].kuryerpos[1] = 10.0f; 

        // перебором находим ближайшего курьера под каждый заказ
        foreach (Zakazi order in orders)
        {
            float minDistance = float.MaxValue; // присваиваем максимальное значение для переменной 
            foreach (Kuryeri courier in couriers)
            {
                float distance = CalculateDistance(order.otkuda, courier.kuryerpos); // отправляем входные данные для нашего метода
                if (distance < minDistance)
                {
                    minDistance = distance;
                    order.optimalCourier = courier;
                }
            }
        }
        // вместо перебора можно было бы логически разбить пространство на отдельные зоны например квадраты,
        // и искать сначала ближайшие к заказу зоны со свободными курьерами и затем среди них находить оптимального.
        // и мб проводить расчеты расстояния в приложении курьера и отправлять данные на сервер чтобы меньше его нагружать(?), если конечно расчеты связанные с геопозицией и тп не делаются через условный api 2gis

        //Выводим в консоль пары заказ + курьер
        foreach (Zakazi order in orders)
        {
            Console.WriteLine($"Order: {order.orderName}, Optimal courier: {order.optimalCourier.courierName}");
        }
    }



}
