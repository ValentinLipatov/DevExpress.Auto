using System;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Parser;
using System.Linq.Expressions;

namespace XML
{
    public static class DynamicLinq
    {
        static DynamicLinq()
        {
            _parsingConfig = new ParsingConfig();
        }

        private static ParsingConfig _parsingConfig;

        public static T Invoke<T>(string expression, string parameterName, Type parameterType, object parameter)
        {
            return (T)Invoke(expression, parameterName, parameterType, parameter, typeof(T));
        }

        public static TResult Invoke<TParameter, TResult>(string expression, string parameterName, TParameter parameter)
        {
            return (TResult)Invoke(expression, parameterName, typeof(TParameter), parameter, typeof(TResult));
        }

        public static object Invoke(string expression, string parameterName, Type parameterType, object parameter, Type resultType)
        {
            var expressionParameter = Expression.Parameter(parameterType, parameterName);
            var expressionParser = new ExpressionParser(new[] { expressionParameter }, expression, new object[] { }, _parsingConfig);
            var body = expressionParser.Parse(resultType);
            return Expression.Lambda(body, expressionParameter).Compile().DynamicInvoke(parameter);
        }
    }
}