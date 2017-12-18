#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class UnityChannelTangle
    {
        private static byte[] data = System.Convert.FromBase64String("yE497KnCPjQeDdiGHfnTt+hsqJNhw9/HnwozcGwh8j+ceeEunmapq9QWtaVGGs7xUUmsfWj7/LyeZipXFz0G2LFXZKVt8RMWhsLFng8g7SJywtTGBbPorecuDPBMD9NRDpz7YTxPA50Du3htHY7/hoFX1aEOyiJ/YxmoBSlxaIxHUBu/1Mytof/4M7OmcEbqOjGeCVIpyYI4mxTr3EzcSOBjbWJS4GNoYOBjY2LKUoDHtWLW+ANt/wmHfqDx9XgKCPyQ/j2vG8hcVhcF2GQvTFAbF7g68rCjP0+rT52G4QvNIA/yuPc6ANZStqa1TfQtUuBjQFJvZGtI5CrklW9jY2NnYmHA7Go+Q1PoFdqtCWzX9P5utadxA+FAV83afD69f2BhY2Jj");
        private static int[] order = new int[] { 12,9,3,3,5,8,13,10,9,11,11,11,12,13,14 };
        private static int key = 98;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
