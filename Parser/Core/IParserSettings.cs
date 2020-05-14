using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Core {
    /// <summary>
    /// Интерфейс описывает настройки парсера.
    /// </summary>
    interface IParserSettings {
        string BaseUrl { get; set; }    // Url сайта, который нужно парсить.

        public string Prefix { get; set; }

        public int StartPoint { get; set; } // С какой страницы парсить данные.

        public int EndPoint { get; set; }   // Конечная страница для парсинга.
    }
}
