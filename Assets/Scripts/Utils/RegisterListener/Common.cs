#if UNITY_EDITOR
#define DEBUG
#define ASSERT
#endif
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;



    public class Commons
    {
        //-----------------------------------
        //--------------------- Log , warning, 

        [Conditional("DEBUG")]
        public static void Log(object message)
        {
            //DebugCustom.Log(message);
        }

        [Conditional("DEBUG")]
        public static void Log(string format, params object[] args)
        {
            //DebugCustom.Log(string.Format(format, args));
        }

        //[Conditional("DEBUG")]
        //public static void LogWarning(object message, Object context)
        //{
        //    FunctionHelper.ShowDebugWarning(message, context);
        //}

        //[Conditional("DEBUG")]
        //public static void LogWarning(Object context, string format, params object[] args)
        //{
        //    FunctionHelper.ShowDebugWarning(string.Format(format, args), context);
        //}



        //[Conditional("DEBUG")]
        //public static void Warning(bool condition, object message)
        //{
        //    //if (!condition) DebugCustom.LogWarning(message);
        //}

        //[Conditional("DEBUG")]
        //public static void Warning(bool condition, object message, Object context)
        //{
        //    if (!condition) FunctionHelper.ShowDebugWarning(message, context);
        //}

        //[Conditional("DEBUG")]
        //public static void Warning(bool condition, Object context, string format, params object[] args)
        //{
        //    if (!condition) FunctionHelper.ShowDebugWarning(string.Format(format, args), context);
        //}


        //---------------------------------------------
        //------------- Assert ------------------------

        /// Thown an exception if condition = false
        [Conditional("ASSERT")]
        public static void Assert(bool condition)
        {
            if (!condition) throw new UnityException();
        }

        /// Thown an exception if condition = false, show message on console's log
        [Conditional("ASSERT")]
        public static void Assert(bool condition, string message)
        {
            if (!condition) throw new UnityException(message);
        }

        /// Thown an exception if condition = false, show message on console's log
        [Conditional("ASSERT")]
        public static void Assert(bool condition, string format, params object[] args)
        {
            if (!condition) throw new UnityException(string.Format(format, args));
        }
    }

