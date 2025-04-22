using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using r_framework.OriginalException;

namespace r_framework.Utility
{
    /// <summary>
    /// ログ出力クラス
    /// </summary>
    public static class LogUtility
    {
        /// <summary>
        /// Log4Net
        /// </summary>
        private static log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 出力を行うメッセージ
        /// </summary>
        public static string Message { get; set; }

        /// <summary>
        /// 呼出メソッド名
        /// </summary>
        public static string MethodName
        {
            get
            {
                return CallerFrame.GetMethod().Name;
            }
        }

        /// <summary>
        /// スタックトレース用のフレーム
        /// </summary>
        public static StackFrame CallerFrame { get; set; }

        /// <summary>
        /// 例外
        /// </summary>
        public static EdisonException Exception { get; set; }

        /// <summary>
        /// 呼出クラス名
        /// </summary>
        private static string ClassName
        {
            get
            {
                return CallerFrame.GetMethod().ReflectedType.FullName;
            }
        }

        /// <summary>
        /// ログ出力先のディレクトリを設定する。
        /// スタートアッププログラム/app.configのDailyFileAppender/file="nul"にしておくとログ出力されない。
        /// このメソッドでディレクトリを設定すると初めてログ出力されるようになる。
        /// １度設定した後でも何度でも変更可能。ログイン時のWindows user&接続DB別にログ出力先を変更するための機能。
        /// </summary>
        public static void SetLogOutputDirectory(string directory)
        {
            foreach (var repository in log4net.LogManager.GetAllRepositories())
            {
                foreach (var appender in repository.GetAppenders())
                {
                    if (appender.Name.Equals("DailyFileAppender"))
                    {
                        var fileAppender = appender as log4net.Appender.FileAppender;
                        if ( fileAppender != null )
                        {
                            fileAppender.File = directory;
                            fileAppender.ActivateOptions();                            
                        }
                    }
                }
            }
        }
    
        /// <summary>
        /// Debugログの出力
        /// </summary>
        [System.Security.DynamicSecurityMethod]
        public static void Debug()
        {
            log.Debug(ClassName + ":" + MethodName + ":" + Message);
        }

        /// <summary>
        /// Debugログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        [System.Security.DynamicSecurityMethod]
        public static void Debug(object message)
        {
            log.Debug(message);
        }

        /// <summary>
        /// Infoログの出力
        /// </summary>
        [System.Security.DynamicSecurityMethod]
        public static void Info()
        {
            log.Info(ClassName + ":" + MethodName + ":" + Message);
        }

        /// <summary>
        /// Infoログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        [System.Security.DynamicSecurityMethod]
        public static void Info(object message)
        {
            log.Info(message);
        }

        /// <summary>
        /// Warnログの出力
        /// </summary>
        [System.Security.DynamicSecurityMethod]
        public static void Warn()
        {
            log.Warn(ClassName + ":" + MethodName + ":" + Message);
        }

        /// <summary>
        /// Warnログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        [System.Security.DynamicSecurityMethod]
        public static void Warn(object message)
        {
            log.Warn(message);
        }

        /// <summary>
        /// Errorログの出力
        /// </summary>
        [System.Security.DynamicSecurityMethod]
        public static void Error()
        {
            log.Error(ClassName + ":" + MethodName + " " + Exception.ErrorCode + ":" + Exception.ErrorMessage);
        }

        /// <summary>
        /// Errorログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        [System.Security.DynamicSecurityMethod]
        public static void Error(object message)
        {
            log.Error(message);
        }

        /// <summary>
        /// Errorログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        /// <param name="ex">Exceptionオブジェクト</param>
        [System.Security.DynamicSecurityMethod]
        public static void Error(object message, Exception ex)
        {
            log.Error(message, ex);
        }

        /// <summary>
        /// Fatalログの出力
        /// </summary>
        [System.Security.DynamicSecurityMethod]
        public static void Fatal()
        {
            log.Fatal(ClassName + ":" + MethodName + " " + Exception.ErrorCode + ":" + Exception.ErrorMessage);
        }

        /// <summary>
        /// Fatalログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        [System.Security.DynamicSecurityMethod]
        public static void Fatal(object message)
        {
            log.Fatal(message);
        }

        /// <summary>
        /// Fatalログの出力
        /// </summary>
        /// <param name="message">出力内容</param>
        /// <param name="ex">Exceptionオブジェクト</param>
        [System.Security.DynamicSecurityMethod]
        public static void Fatal(object message, Exception ex)
        {
            log.Fatal(message, ex);
        }

        /// <summary>
        /// メソッドの開始を Debug ログに出力
        /// </summary>
        /// <param name="args">該当メソッドの引数（全指定。引数がない場合は指定なし）</param>
        [System.Security.DynamicSecurityMethod]
        public static void DebugMethodStart(params object[] args)
        {
            var stackFrame = new StackFrame(1);
            var methodBase = stackFrame.GetMethod();

            var methodName = MakeMethodName(methodBase);
            var format = MakeArgumentFormat(methodBase);
            log.Debug(methodName + MessageFormated(methodName, format, args));
        }

        /// <summary>
        /// メソッドの終了を Debug ログに出力
        /// </summary>
        /// <param name="args">
        /// <para>戻り値（void の場合は指定なし）</para>
        /// <para>ref は指定する、outは指定しない</para>
        /// </param>
        [System.Security.DynamicSecurityMethod]
        public static void DebugMethodEnd(params object[] args)
        {
            var stackFrame = new StackFrame(1);
            var methodBase = stackFrame.GetMethod();

            var methodName = MakeMethodName(methodBase);
            var format = MakeReturnFormat(methodBase);
            log.Debug(methodName + MessageFormated(methodName, format, args));
        }

        /// <summary>
        /// ログ用メソッド名の作成
        /// </summary>
        /// <param name="methodBase">実行メソッド</param>
        /// <returns>ログ用メソッド名</returns>
        private static string MakeMethodName(MethodBase methodBase)
        {
            return String.Format(
                            "[{0}.{1}.{2}] ",
                            methodBase.DeclaringType.Namespace,
                            methodBase.DeclaringType.Name,
                            methodBase.Name);
        }

        /// <summary>
        /// ログ用引数フォーマット作成
        /// </summary>
        /// <param name="methodBase">実行メソッド</param>
        /// <returns>ログ用引数フォーマット</returns>
        private static string MakeArgumentFormat(MethodBase methodBase)
        {
            var sb = new StringBuilder("Method Enter Argument: ");
            bool first = true;
            foreach (ParameterInfo paramInfo in methodBase.GetParameters())
            {
                if (paramInfo.IsOut)
                {
                    continue;
                }
                if (!first)
                {
                    sb.Append(", ");
                }
                else
                {
                    first = false;
                }

                if (paramInfo.ParameterType.IsByRef)
                {
                    sb.Append("ref ");
                }
                //else if (paramInfo.ParameterType.IsGenericType)
                //{
                //    ;
                //}
                sb.Append(paramInfo.ParameterType.Name);
                sb.Append(" ");
                sb.Append(paramInfo.Name);
                sb.AppendFormat("=[{{{0}}}]", paramInfo.Position);
            }
            if (first)
            {
                sb.Append("Void");
            }
            return sb.ToString();
        }

        /// <summary>
        /// ログ用メッセージフォーマット作成
        /// </summary>
        /// <param name="methodName">実行メソッド名</param>
        /// <param name="format">出力文字フォーマット</param>
        /// <param name="args">引数</param>
        /// <returns>ログ用メッセージフォーマット</returns>
        private static string MessageFormated(string methodName, string format, params object[] args)
        {
            var logFormat = format ?? String.Empty;

            var list = new List<string>();
            if (args == null)
            {
                list.Add("null");
            }
            else
            {
                foreach (var obj in args)
                {
                    list.Add(LogArgsStringMaker.MakeLogString(obj));
                }
            }
            try
            {
                if (list.Count == 0)
                {
                    return String.Format(logFormat, "Count=0");
                }
                else
                {
                    return String.Format(logFormat, list.ToArray());
                }
            }
            catch (FormatException ex)
            {
                var badParameter = String.Format("format=[{0}], args=[{1}]", logFormat, string.Join(", ", list.ToArray()));
                log.Warn(methodName + badParameter, ex);
                throw;
            }
        }

        /// <summary>
        /// ログ用戻り値フォーマット作成
        /// </summary>
        /// <param name="methodBase">実行メソッド名</param>
        /// <returns>ログ用戻り値フォーマット</returns>
        private static string MakeReturnFormat(MethodBase methodBase)
        {
            var sb = new StringBuilder();
            try
            {
                if (methodBase.IsConstructor || (methodBase.MemberType == MemberTypes.Constructor))
                {
                    sb.Append("Constructed: ").Append("ClassValue=[{0}]");
                    string n = ((MemberInfo)methodBase).ToString();
                    MemberTypes ty = ((ConstructorInfo)methodBase).MemberType;
                    string str = ((ConstructorInfo)methodBase).DeclaringType.UnderlyingSystemType.ToString();
                }
                else
                {
                    sb.Append("Method Exit: ");
                    var index = 0;
                    var returnType = ((MethodInfo)methodBase).ReturnType.Name;
                    sb.Append("ReturnValue(").Append(returnType);
                    if (returnType != Type.GetType("System.Void").Name)
                    {
                        sb.Append("=[{0}]");
                        ++index;
                    }
                    sb.Append(")");

                    foreach (var paramInfo in methodBase.GetParameters())
                    {
                        if (paramInfo.IsOut)
                        {
                            sb.Append(", out ");
                        }
                        else if (paramInfo.ParameterType.IsByRef)
                        {
                            sb.Append(", ref ");
                        }
                        else
                        {
                            continue;
                        }

                        sb.Append(paramInfo.ParameterType.Name).Append(" ").Append(paramInfo.Name);
                        sb.AppendFormat("=[{{{0}}}]", index);
                        ++index;
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.Warn(MethodBase.GetCurrentMethod() + ex.Message, ex);
            }
            return sb.ToString();
        }

        public static string GetTraceInfo(System.Reflection.MethodBase m, params object[] args)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendFormat("{0}.{1}(", m.DeclaringType.Name, m.Name);
            bool first = true;
            foreach (var p in args)
            {
                if (!first)
                    sb.Append(", ");
                first = false;
                if (p == null) sb.Append("null");
                else
                {
                    var type = p.GetType();
                    if (type == typeof(string)) sb.AppendFormat("\"{0}\"", p.ToString());
                    else if (type == typeof(char)) sb.AppendFormat("\'{0}\'", p.ToString());
                    else sb.Append(p.ToString());
                }
            }
            sb.Append(')');
            return sb.ToString();
        }
    }

    /// <summary>
    /// ログ用引数文字列生成クラス
    /// </summary>
    internal static class LogArgsStringMaker
    {
        /// <summary>
        /// ログ用引数文字列生成
        /// </summary>
        /// <param name="target">対象引数</param>
        /// <returns>ログ用引数文字列</returns>
        public static string MakeLogString(object target)
        {
            if (target == null)
            {
                return null;
            }
            else if (target is IList)
            {
                return MakeListLogString((IList)target);
            }
            else if (target is IDictionary)
            {
                return MakeDictionaryLogString((IDictionary)target);
            }
            else if (target is IEnumerable && !(target is string))
            {
                return MakeEnumerableLogString((IEnumerable)target);
            }
            else
            {
                return target.ToString();
            }
        }

        /// <summary>
        /// List用
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static string MakeListLogString(IList target)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(
                String.Join(", ", target.OfType<object>().Select(item => MakeLogString(item)))
                );
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Dictionary用
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static string MakeDictionaryLogString(IDictionary target)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(
                String.Join(
                    ", ", target.Keys.OfType<object>()
                                .Select(key => MakeLogString(key)
                                + " = "
                                + MakeLogString(target[key])))
                );
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// IEnumerable用
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static string MakeEnumerableLogString(IEnumerable target)
        {
            return MakeListLogString(target.OfType<object>().ToList());
        }
    }
}

namespace System.Security
{
    /// <summary>
    /// インライン化させないための拡張属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class DynamicSecurityMethodAttribute : Attribute
    {
    }
}
