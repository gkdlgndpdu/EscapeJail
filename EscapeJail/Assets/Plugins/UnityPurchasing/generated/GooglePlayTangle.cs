#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("2qARvJDI0TX+6aIGbXUUGEZBigp5VdOH+upRrGMUsNVuTUfXDB7IuutZ2vnr1t3S8V2TXSzW2tra3tvYJD9YsnSZtksBToO5b+sPHwz0TZSF9rokugLB1KQ3Rj847mwYt3Obxth6Zn4ms4rJ1ZhLhiXAWJcn3xASba8MHP+jd0jo8BXE0UJFBSffk+4fyf9Tg4gnsOuQcDuBIq1SZfVl8Vna1NvrWdrR2Vna2ttz6zl+DNtvcfeEVRB7h42ntGE/pEBqDlHVESpButRGsD7HGUhMwbOxRSlHhBaicct7bX+8ClEUXpe1SfW2aui3JULYroS/YQju3RzUSKqvP3t8J7aZVJvl7668Yd2W9emirgGDSwkahvYS9lj57nRjxYcExtnY2tva");
        private static int[] order = new int[] { 13,6,13,7,8,9,7,10,10,12,11,12,13,13,14 };
        private static int key = 219;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
