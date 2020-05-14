using AngleSharp.Html.Parser;
using System;

namespace Parser.Core {
    /// <summary>
    /// Сервис описывает работу парсера.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ParserWorker<T> where T : class {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        // Событие возвращает спарсенные с итерации данные.
        public event Action<object, T> OnNewData;

        // Событие отвечает за информирование при завершении работы парсера.
        public event Action<object> OnCompleted;

        public IParser<T> Parser {
            get {
                return parser;
            }
            set {
                parser = value;
            }
        }

        public IParserSettings Settings {
            get {
                return parserSettings;
            }
            set {
                parserSettings = value;
                loader = new HtmlLoader(value); // Создает новый экземпляр со свойствами парсера.
            }
        }

        public bool IsActive {
            get {
                return isActive;
            }
        }

        public ParserWorker(IParser<T> parser) {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser) { 
            // this(parser) Вызывает первый конструктор и передает ему парсер.
            this.parserSettings = parserSettings;
        }

        // Метод для старта парсера.
        public void Start() {
            isActive = true;
            Worker();
        }

        // Метод для остановки парсера.
        public void Abort() {
            isActive = false;
        }

        // Метод контролирует процесс парсинга.
        async void Worker() {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++) {
                if (!isActive) {
                    // Если работа парсера была остановлена.
                    OnCompleted?.Invoke(this);
                    return;
                }

                // Получает исходный код страницы с индексом из цикла.
                var source = await loader.GetSourceByPage(i);
                var domParser = new HtmlParser();

                // Парсит асинхронно код и получает страницу с которой можно работать.
                var document = await domParser.ParseDocumentAsync(source);

                // Передает парсеру документ и получает спарсенные данные.
                var result = parser.Parse(document);

                // Передает ссылку и результат.
                OnNewData(this, result);
            }

            // Если парсер закончил работу.
            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
