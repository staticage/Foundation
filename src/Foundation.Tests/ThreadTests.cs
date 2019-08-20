using System;
using System.Threading;
using Foundation.Core;
using NUnit.Framework;

namespace Tests
{
    public class ThreadTests
    {
        [Test]
        public void NormalTest()
        {
            1000.Times(() => new Normal().Test());
        }
    }

    public class Normal
    {
        private int m_flag = 0;
        private int m_value = 0;
        // This method is executed by one thread
        public void Thread1() {        
            // Note: 5 must be written to m_value before 1 is written to m_flag
            m_value = 5;
            m_flag = 1;
        }

        // This method is executed by another thread
        public void Thread2() {        
            // Note: m_value must be read after m_flag is read
            if (m_flag == 1)
            {
                Console.Write(m_value);
            }
        }

        public void Test()
        {
            new Thread(Thread1).Start();
            new Thread(Thread2).Start();
        }
    }
}