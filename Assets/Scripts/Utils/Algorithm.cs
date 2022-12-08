using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonsterUtils
{
    public static class Algorithm
    {

        private static readonly Regex _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");

        public static bool HasContent(this string source) =>
          !String.IsNullOrWhiteSpace(source);

        public static bool IsEmpty(this string source) =>
          String.IsNullOrWhiteSpace(source);

        public static int ToInt(this string str) => int.Parse(str);

        public static float ToFloat(this string str) => float.Parse(str);

        public static bool OnlyCharactersIn(this string source,
          string allowedChars) => source.ToCharArray().All(allowedChars.Contains);

        public static T ClampTo<T>(this T source, T minValue, T maxValue)
          where T : IComparable => source.CompareTo(minValue) < 0 ? minValue :
          source.CompareTo(maxValue) > 0 ? maxValue :
          source;

        public static int Difference(this int source, int target) =>
          Math.Abs(source - target);

        public static (T1, T2) MakeTuple<T1, T2>(this T1 item1, T2 item2) =>
          (item1, item2);

        public static (T1, T2, T3) MakeTuple<T1, T2, T3>(this T1 item1,
          T2 item2,
          T3 item3) =>
          (item1, item2, item3);

        public static (T1, T2) MakeTupleAppend<T1, T2>(this T2 item2, T1 item1) =>
          (item1, item2);

        public static (T1, T2, T3) MakeTupleAppend<T1, T2, T3>(this T3 item3,
          T1 item1,
          T2 item2) => (item1, item2, item3);

        public static T PassTo<T>(this T argument, Action<T> action)
        {
            action(argument);
            return argument;
        }

        public static (T1, T2) PassTo<T1, T2>(this (T1, T2) arguments,
          Action<T1, T2> action)
        {
            action(arguments.Item1, arguments.Item2);
            return arguments;
        }

        public static (T1, T2, T3) PassTo<T1, T2, T3>(this (T1, T2, T3) arguments,
          Action<T1, T2, T3> action)
        {
            action(arguments.Item1, arguments.Item2, arguments.Item3);
            return arguments;
        }

        public static R PassTo<T, R>(this T argument, Func<T, R> function) =>
          function(argument);

        public static R PassTo<T1, T2, R>(this (T1, T2) arguments,
          Func<T1, T2, R> function) =>
          function(arguments.Item1, arguments.Item2);

        public static R PassTo<T1, T2, T3, R>(this (T1, T2, T3) arguments,
          Func<T1, T2, T3, R> function) =>
          function(arguments.Item1, arguments.Item2, arguments.Item3);

        public static IEnumerable<R> SelectByIndex<T, R>(this IEnumerable<T> source,
          Func<int, R> selector)
        {
            IEnumerable<R> result = Enumerable.Empty<R>();
            int sourceLength = source.Count();
            for (int i = 0; i < sourceLength; ++i)
                result = result.Append(selector(i));
            return result;
        }

        public static R ConvertBy<T, R>(this T source, Func<T, R> converter) =>
          converter(source);

        public static Nullable<T> NullIf<T>(this T source, bool condition)
          where T : struct =>
          condition ? null : new Nullable<T>(source);

        public static Nullable<T> NullIf<T>(this T source, Predicate<T> condition)
          where T : struct =>
          condition(source) ? null : new Nullable<T>(source);

        public static void ProceedIfMatchType<TC>(this System.Object source,
          Action<TC> action) where TC : class
        { if (source is TC) action(source as TC); }

        public static IDictionary<TKey, TValue> Replace<TKey, TValue>(
          this IDictionary<TKey, TValue> source,
          TKey key,
          TValue value,
          Action<TValue> actionForExistingValue = null)
        {
            if (source.ContainsKey(key)) actionForExistingValue?.Invoke(source[key]);
            source[key] = value;
            return source;
        }

        public static IEnumerable<T> FillToSize<T>(this IEnumerable<T> source,
          int size,
          Func<int, T> generator)
        {
            int sourceStartLength = source.Count();
            var additions = Enumerable.Range(sourceStartLength,
              size - sourceStartLength)
              .Select(generator);
            return source.Concat(additions);
        }

        public static ICollection<T> FillToSize<T>(this ICollection<T> source,
          int size,
          Func<int, T> generator)
        {
            int sourceStartLength = source.Count;
            for (int i = sourceStartLength; i < size; ++i)
                source.Add(generator(i));
            return source;
        }

        public static IEnumerable<KeyValuePair<TK, RV>> ValueSelect<TK, TV, RV>(
          this IDictionary<TK, TV> source,
          Func<TV, RV> valueSelector) =>
          source.Select(pair => new KeyValuePair<TK, RV>(pair.Key,
            valueSelector(pair.Value)));

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(
          this IEnumerable<KeyValuePair<TKey, TValue>> source) =>
          source.ToDictionary(pair => pair.Key, pair => pair.Value);

        public static T LoopingElementAt<T>(this IEnumerable<T> source, int i) =>
          source.ElementAt(i % source.Count());

        public static IEnumerable<int> IndicesWhere<T>(this IEnumerable<T> source,
          Predicate<T> predicate)
        {
            var indices = Enumerable.Empty<int>();
            int sourceLength = source.Count();
            for (int i = 0; i < sourceLength; ++i)
                if (predicate(source.ElementAt(i))) indices = indices.Append(i);
            return indices;
        }

        public static IEnumerable<int> IndicesWhere<T>(this IList<T> source,
          Predicate<T> predicate)
        {
            var indices = Enumerable.Empty<int>();
            for (int i = 0; i < source.Count; ++i)
                if (predicate(source[i])) indices = indices.Append(i);
            return indices;
        }

        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> source,
          T element) => source.Where(se => !se.Equals(element));

        public static Random Randomizer = new Random();

        public static int Multiply(this int source, int right) => source * right;

        public static float Multiply(this int source, float right) =>
          source * right;

        public static float Multiply(this float source, float right) =>
          source * right;

        public static float Plus(this float source, float right) =>
          source + right;

        public static int Plus(this int source, int right) => source + right;

        public static float Sqrt(this float source) => (float)Math.Sqrt(source);

        public static float Pow(this float source, float power) =>
          (float)Math.Pow(source, power);

        public static int Round(this float source) => (int)Math.Round(source);

        public static int FloorToInt(this float source) => (int)Math.Floor(source);

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) =>
          source.OrderBy(e => Randomizer.Next());

        public static IEnumerable<T> ValuesOf<T>() =>
          Enum.GetValues(typeof(T)).Cast<T>();

        public static IReadOnlyDictionary<string, string> ParseQueryString(this Uri uri)
        {
            var match = _regex.Match(uri.PathAndQuery);
            var paramaters = new Dictionary<string, string>();
            while (match.Success)
            {
                paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
                match = match.NextMatch();
            }
            return paramaters;
        }
    }

    public static class Algorithm2
    {
        public static T NullIf<T>(this T source, bool condition)
          where T : class =>
          condition ? null : source;

        public static T NullIf<T>(this T source, Predicate<T> condition)
          where T : class =>
          condition(source) ? null : source;
    }

    [Serializable]
    public class SerializableInts
    {
        public List<int> Values = new List<int>();
    }
}