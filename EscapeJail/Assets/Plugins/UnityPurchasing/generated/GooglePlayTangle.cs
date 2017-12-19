#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("nS+sj52gq6SHK+UrWqCsrKyora4PI6XxjJwn2hVixqMYOzGhemi+zFJJLsQC78A9dzj1zxmdeWl6gjvik5nYyher4IOf1Nh39T1/bPCAZICuDBAIUMX8v6PuPfBTti7hUalmZKzWZ8rmvqdDiJ/UcBsDYm4wN/x8L6yirZ0vrKevL6ysrQWdTwh6rRkHgfIjZg3x+9HCF0nSNhx4J6NnXPOAzFLMdLei0kEwSU6YGm7BBe2w2PLJF36Yq2qiPtzZSQ0KUcDvIu03zKIwxkixbz46t8XHM18x8mDUBxvZemqJ1QE+noZjsqc0M3NRqeWYab+JJfX+Ucad5gZN91TbJBODE4e9DRsJynwnYijhwz+DwByewVM0ri6PmAIVs/FysK+urK2s");
        private static int[] order = new int[] { 0,6,7,4,9,13,6,12,8,12,11,12,12,13,14 };
        private static int key = 173;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
