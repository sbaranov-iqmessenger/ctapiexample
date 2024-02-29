using System.Collections.Generic;

namespace CtApiExample.CtAPI
{
    /// <summary>
    ///     Encapsulates the correlation of Character set numbers to codepage numbers and conversions therein.
    /// </summary>
    /// <remarks>
    ///     The content in this class is based off a Microsoft knowledge base article:
    ///     http://support.microsoft.com/kb/165478
    /// </remarks>
    internal class CtApiCharsetHelper
    {
        private readonly int[][] _defaultCharsetsToCodepages =  {      /*Charset, Codepage */
                        new[] {   0, 1252}, // ANSI_CHARSET      - Latin 1 languages: Afrikaans, Basque, Catalan, Danish, Dutch, English, Faroese, Finnish, French, Galician, German, Icelandic, Indonesian, Italian, Malay, Norwegian, Portuguese, Spanish, Swahili, Swedish    
                        new[] { 204, 1251 },// RUSSIAN_CHARSET   - Cyrillic languages: Azeri, Belarusian, Bulgarian, FYRO Macedonian, Kazakh, Kyrgyz, Mongolian, Russian
                        new[] { 238, 1250}, // EE_CHARSET        - Central Europe languages: Albanian, Croatian, Czech, Hungarian, Polish, Romanian, Serbian (Latin), Slovak, Slovenian       
                        new[] { 161, 1253}, // GREEK_CHARSET     - Greek      
                        new[] { 162, 1254}, // TURKISH_CHARSET   - Turkic languages: Azeri (Latin), Turkish, Uzbek (Latin)
                        new[] { 186, 1257}, // BALTIC_CHARSET    - Baltic languages: Estonian, Latvian, Lithuanian  
                        new[] { 177, 1255}, // HEBREW_CHARSET    - Hebrew   
                        new[] { 178, 1256}, // ARABIC _CHARSET   - Arabic, Farsi, Urdu 
                        new[] { 128,  932}, // SHIFTJIS_CHARSET  - Japanese
                        new[] { 129,  949}, // HANGEUL_CHARSET   - Korean 
                        new[] { 134,  936}, // GB2313_CHARSET    - Chinese Simplified    
                        new[] { 136,  950}, // CHINESEBIG5_CHARSET   - Traditional Chinese 
                                                      };

        private readonly Dictionary<int, int> _charsetsToCodePages = new Dictionary<int, int>();

        #region Constructors -----------------------------------------------------

        /// <summary>
        ///     Constructor, sets up a dictionary that maps Charset IDs to CodePage IDs.
        /// </summary>
        public CtApiCharsetHelper()
        {
            InitializeDefaultLanguageIDsToCodePages();
        }

        #endregion // Constructors

        #region Public Methods ---------------------------------------------------

        /// <summary>
        ///     Returns the associated codepage for a specified charset.
        /// </summary>
        /// <param name="charSet">
        ///     The Charset ID to get a CodePage ID for.
        /// </param>
        /// <returns>
        ///     The CodePage ID that this Charset uses, or 0 if unknown. 
        ///     (when 0 is passed to Encoding.GetEncoding then it returns the os codepage)
        /// </returns>
        public int GetCodePage(int charSet)
        {
            int codePage;

            _charsetsToCodePages.TryGetValue(charSet, out codePage); // This defaults codePage to 0 if there is an error.

            return codePage;
        }

        #endregion // Public Methods

        #region Private Methods --------------------------------------------------

        private void InitializeDefaultLanguageIDsToCodePages()
        {
            foreach (int[] defaultCharsetsToCodepage in _defaultCharsetsToCodepages)
            {
                _charsetsToCodePages.Add(defaultCharsetsToCodepage[0], defaultCharsetsToCodepage[1]);
            }
        }

        #endregion // Private Methods
    }

}
