using System;
using System.Collections.Generic;

namespace _002_observer_pattern
{
    class Program
    {

        // 観察対象者
        internal abstract class Subject {

            // 観測者のリスト
            protected List<Observer> m_observerList = null;

            // コンストラクタ
            internal Subject() {
                m_observerList = new List<Observer>();
            }

            // 観測者の紐付け
            internal void Attach(Observer observer) {
                m_observerList.Add(observer);
            }

            // 観測者の解除
            internal void Detouch(Observer observer) {
                m_observerList.Remove(observer);
            }

            // 観測者へ通知する
            internal void Notify() {
                foreach (Observer tmp in m_observerList) {
                    tmp.Update();
                }
            }
        }

        // 観測者
        internal abstract class Observer {

            // 更新する
            internal abstract void Update();
        }

        // 温度計(観察対象者)
        internal class Thermometer : Subject{

            // 温度
            private int m_temperature = 0;

            // プロパティ
            internal int Temperature {
                get { return m_temperature; }
                set { m_temperature = value; }
            }
        }

        // チェッカー
        internal class Checker : Observer {

            // 温度
            private int m_temperature = 0;
            private int m_thermometerTemperature = 0;

            // 温度計
            private Thermometer m_thermometer = null;

            // コンストラクタ
            internal Checker(Thermometer thermometer, int temperature) {
                m_thermometer = thermometer;
                m_temperature = temperature;
            }

            // 更新する
            internal override void Update() {
                m_thermometerTemperature = m_thermometer.Temperature;
                Console.WriteLine(" Checker {0} new state is {1}", m_temperature, m_thermometer.Temperature);
            }

            // プロパティ
            internal Thermometer Thermometer{
                get { return m_thermometer; }
                set { m_thermometer = value; }
            }
        }

        // メイン関数
        static void Main(string[] args) {

            // 温度計を生成する
            Thermometer homeThermometer = new Thermometer();

            // チェッカーを生成する
            Checker outside = new Checker(homeThermometer, 10);
            Checker inside = new Checker(homeThermometer, 20);

            // 通知者を紐付ける
            homeThermometer.Attach(outside);
            homeThermometer.Attach(inside);

            // 温度を変える
            homeThermometer.Temperature = 40;

            // 観測者へ通知する
            homeThermometer.Notify();
        }
    }
}
