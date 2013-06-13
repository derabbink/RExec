using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Plugin
{
    public class DependencyResolver
    {
        static string greetings = "PONG!";

        public static void GetAllDependencies()
        {
            AppDomain otherDomain = AppDomain.CreateDomain("otherDomain");
            

            greetings = "PING!";
            MyCallBack();
            //this does not quite work with NUnit
            otherDomain.DoCallBack(new CrossAppDomainDelegate(MyCallBack));
            AppDomain.Unload(otherDomain);
        }

        static public void MyCallBack()
        {
            string name = AppDomain.CurrentDomain.FriendlyName;

            if (name == AppDomain.CurrentDomain.SetupInformation.ApplicationName)
            {
                name = "defaultDomain";
            }
            Console.WriteLine(greetings + " from " + name);
        }
    }
}
