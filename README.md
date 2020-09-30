# TaskManager

**I'm not offering any license** | **Nie udzialam żadnej licencji**

Strona wdrożona na Azure: https://taskmanagerweb20200911175909.azurewebsites.net/

Role w systemie:
* Admin
* Serwisant
* Klient

Rola jest nadawana użytkownikowi w zależności od tego do jakiego kontrahenta jest przypisany. Klienci widzą zadanie przypisane jedynie do danego kontrahenta. Serwisanci widzą wszystkie zadania. Klienci i Serwisanci mają różne zakresy statusów do przebijania. Serwisant może wybrać kontrahenta i przedstawiciela przy tworzeniu zgłoszenia, a klient nie może. Serwisant może zmieniać wykonawcę zgłoszenia i ustawiać budżet (którego klient nie widzi), a klient może ustawiać priorytet. Admin może na przykład zmieniać i usuwać wszystko co zechce. Widoczność przycisków jest zależna od roli i stanu zgłoszenia, na przykład jako "Do odbioru" przebijać może jedynie osoba przypisana jak wykonująca. Oczywiście po stronie API jest walidacja, która nie pozwoli na przebicie na określony status, jeśli użytkownik lub zadanie nie spełniają określonych wymogów. To samo jest z dostępem do danych, klient może pobrać jedynie kontrahenta do którego jest przypisany i pracowników jedynie przypisanych do tego kontrahenta. Serwisanci i Admini mogą pobrać wszystkich itd.

_Dane kontrahentów potrzebne przy rejestracji_:

```csharp
new Contrahent()
{
    Name = "IBM Serwisant",
    NIP = "1234567890",
    IsOperator = true,
    LicenseKey = "12345678901234567890"
},
new Contrahent()
{
    Name = "Microsoft Klient",
    NIP = "1234567891",
    IsOperator = false,
    LicenseKey = "12345678911234567891"
}
```
> The https://github.com/robots.txt file of GitHub allows the indexing of the blobs in the 'master' branch, but restricts all other branches. So if you don't have a 'master' 
> branch, Google is not supposed to index your pages.

## Wstęp

Oprócz wytwarzanie oprogramowania, innym elementem działalności firm informatycznych jest obsługa zgłoszeń serwisowych w których najczęściej zawarte są problemy klientów z oprogramowaniem, prośby o dodanie danych funkcjonalności do oprogramowania czy pytania typowo biznesowe. W toku działalności firmy informatycznej jej zarząd często podejmuje decyzję o usprawnieniu obsługi zgłoszeń poprzez wdrożenie portalu zgłoszeniowego który będzie narzędziem komunikacji klientów, jak i pracowników firmy (programistów, zarządu i osób odpowiedzialnych za obsługę klienta). Celem takiego systemu jest także wspomaganie zarządzania zadaniami pracowników w firmie informatycznej.

## Technologia

Single Page Application utworzone z wykorzystaniem knockout.js, HTML, CSS, Bootstrap 4.5, JavaScript z wykorzystaniem najnowszej ECMY, JQuery, inne pomniejsze biblioteki. Jest to aplikacja ASP.NET MVC 5 korzystająca z Entity Framework i komunikująca się z REST API powstałe w ASP.NET WebApi.

## Funkcjonalności

* Dodawanie zadań 
* Podgląd zadań 
* Wyszukiwanie zadań 
* Dodawanie komentarzy do zadań
* Edycja zadań
* Przebijanie statusów w toku realizacji

## Cechy

*   Walidacja po stronie klienta, a także po stronie serwera.
*   Wykorzystuje wzorzec Repository, UOW, Strategy, Command, Chain of Responsibility, IoC, Factory itd.
*   API jest wersjonowane, w response raportowana jest wersja. Domyślną wersją jest 1.1. Możliwe jest określenie innej jako query string lub nagłówek.
*	Obsłużona została edycja równoległa w przyjazny dla użytkownika sposób, np. przy edycji widoczne są wartości jakie ustawił inny użytkownik po pobraniu danych przez danego użytkownika,
*	APi posiada liczne funkcjonalności i operacje dostępne tylko dla admina, np. szyfrowanie i aktualizacja konfiguracji, edycja wszystkich danych, pobieranie wielokryterialne danych w różnej formie. Nie ma jednak skrajności typu RPC.
*	Aplikacja składa się z warstwy biznesowej, dostępu do danych i warstwy webowej
*	Web API korzysta z repozytoriów, które działają w oparciu o ustaloną strategię dostępu do danych opartą o role i cechy użytkowników systemu. Każdy użytkownik może edytować określone dane i musi spełnić dane warunki by na przykład utworzyć zgłoszenie czy usunąć komentarz. Sprawia to, że z API będą mogły korzystać także inne aplikacje, np. okienkowe bez zbytniego przywiązywania uwagi do logiki biznesowej i obsługi poświadczeń
*	Dostęp do API jest autoryzowany tokenem. Na stronie jest zaimplementowana logika tworzenia Bearer Token dla danego użytkownika i odświeżania tokenów.
*	API jest aktualnie wykorzystywane jedynie na potrzeby strony, jednak w planach jest otwarcie dostępu dla innych typów aplikacji – okienkowych czy mobilnych.
*	Zadania i komentarze są tworzone z wykorzystaniem edytora tekstu.
*	Aplikacja tworzy logi z wykorzystaniem Log4Net. Możliwy jest podgląd logów, np. z pzoziomu Cloud Explorer w Visual Studio – logi trafiają do pliku w podfolderze logs aplikacji
*	Możliwe jest aktywowanie różnych typów logów
*	Możliwe jest włączenie logów wszystkich zapytań i danych o ich wykonaniu
*	Możliwe jest włączenie logów do bazy danych
*	Aplikacja na bieżącym etapie jest w pełni funkcjonalna, jednak wymaga rozwoju
*	Aplikacja zbiera dane o logujących się użytkownikach
*	Aplikacja obsługuje różne strefy czasowe, każdy użytkownik widzi odpowiedni dla jego kultury format daty dostosowany do jego strefy czasowej. Obsługiwane są dwa języki interfejsu – angielski i polski.
*	API zwraca daty w formie UTC ISO, a przyjmuje w każdej formie. Jeśli jest to format ISO, to dostosowuje je do UTC. Jeśli jest to format inny niż ISO, to następuje parsowanie daty z wykorzystaniem kultury użytkownika i korekta o timezone offset wysłany przez  użytkownika API poprzez nagłówek lub plik cookie. Jeśli timezone offset nie został wysłany, to data jest traktowana jako UTC
*	W bazie danych są przechowywane jedynie daty UTC.
*	Użytkownik może filtrować zadanie z wykorzystaniem formatu daty odpowiedniego dla jego kultury i strefy czasowej. Po stronie REST API jego data jest odpowiednio dostosowywana do UTC i w ten sposób są pobierane odpowiednie dane.
*	Automatyczne tworzenie numerów ticketów z wykorzystaniem triggera.
*	Aplikacja wykorzystuje bibliotekę programistyczną utworzonej w toku mojej programistycznej przygody. Biblioteka jest na bieżącym etapie dość obszerna i stopniowo rozwijana. Panuje w niej lekki chaos, więc warto jakoś go ujarzmić.
*	Aplikacja korzysta z AutoMappera. Wykorzystuje przede wszystkim projekcję IQueryable, dzięki czemu w prosty sposób tworzone są wydajne zapytania z wielu tabel bez zbędnego i żmudnego kodowania wyrażeń LinQ.
*	Aplikacja jest wyposażona w klienta email
*	Aplikacja korzysta z podmodułów (przede wszystkim z poziomu GitHub)
*	Aplikacja wyświetla błędy w przyjazny dla użytkownika sposób
*	Aplikacja loguje i obsługuje wszystkie błędy w aplikacji, w niektórych miejscach lokalnie, a w innych globalnie
*	Aplikacja wykorzystuje IoC z wykorzystaniem AutoMappera. Dzięki licznym interfejsom, aplikacja jest testowalna i w szybki sposób możliwe jest zastępowanie implementacji bez zmian w wielu miejscach,
*	Tłumaczenie nie dotyczy tylko interfejsu, ale także generycznych danych pochodzących z bazy danych czy komunikatów o błędach.
*	Strona pod kątem interfejsu niemal w całości obsługuje dwa języki: polski i angielski.

## Planowane zmiany

*	Bardziej modułowa budowa po stronie JS, np. z wykorzystaniem require.js. Będą moduły dostępu do danych, router, bootstrapper itd.
*	Dodanie change trackingu po stronie knockout i wysyłanie żądań do API tylko w przypadku edycji jakichkolwiek danych przez użytkownika. Ostrzeganie przed wyjściem z zadania przed zapisaniem danych.
*	Całkowite wyeliminowanie alertów i dodanie zamiast tego dialogów współpracujących z knockout
*	Implementacja Event Stormingu i CQRS
*	Otworzenie dostępu do API dla innych klientów, a nie tylko aplikacji webowej
*	Zgodnie z punktem poprzednim, dodanie weryfikacji client id, request url, a także danych logowania wysyłanych na url pozwalające na utworzenie tokenu.
*	Dodanie bazy wiedzy i przypisywania wpisów do zadań
*	Dodanie modułu planowania i harmonogramowania zadań
*	Dodanie modułu raportów
*	Utworzenie aplikacji WPF czerpiącej z praktyk programowania asynchronicznego z wykorzystaniem Caliburn.Micro. Utworzenie aplikacji w Android Studio. Obie aplikacje będą wersjami portalu kolejno okienkowymi i mobilnymi i będą korzystały z utworzonego Web API.
*	Dodanie pobierania skryptów i stylów z CDN z fallbackiem do serwera celem zmniejszenia wykorzystania zasobów chmury.
*	Przepisanie aplikacji na Angular. Knockout jest dość przestarzałą, ubogą i już niewspieraną, nierozwijaną biblioteką. Rozważyć dość modne ostatnio Vue.js. Rozważyć też Ract, który zdaje się stopniowo wypierać Angulara.
*	Czerpać ze wzorców programowania reaktywnego
*	Wdrożyć komunikator, pomyśleć nad wykorzystaniem bazy NoSQL – ORM nadal jako Entity Framework.
*	Wyeliminować dość niewygodne dla użytkownika odświeżanie UI, przede wszystkim CDEditor. Zbadać flow routingu.
*	Nieco zadbać nad czystością kodu i wdrożyć SRP przede wszystkim jeśli chodzi o przebijanie statusów. W innych miejscach kod jest nawet czysty i wygląda ok.
*	Pokrycie aplikacji testami jednostkowymi i integracyjnymi. Obecnie ilość testów jest bardzo znikoma. Dość sporo testów jest związanych z biblioteką programistyczną (PDCory), ale z samym TaskManagerem już tak średnio.
*	Wykorzystywać strefy czasowe zamiast timezone offsetów. Dodać możliwość ustawienia strefy czasowej przez użytkownika w szczegółach profilu.
*	Dodanie avatarów użytkowników
*	Dodanie obsługi załączników w komentarzach i ticketach
*	Dodanie możliwości edycji większej ilości danych w ticketach, np. opisu czy tematu.
*	Zadbać nad brakiem możliwości edycji ticketów po ich zakończeniu
*	Dodać więcej statusów ticketów, np. „Do zatwierdzenia”, „Zatwierdzone”, „Do wypuszczenia”, „Wymagany wpis do bazy wiedzy”, „Wymagane dane techniczne”.
*	Dodanie do ticketów sekcji z danymi technicznymi do wykorzystania przez serwisantów
*	Dodanie możliwość tworzenia kontrahentów
*	Dodanie weryfikacji pracowników przez kontrahentów – sam kod licencyjny i NIP to za mało, by uzyskać dostęp do portalu
*	Przejść na ASP.NET Core  i Entity Framework Core. Są zalecane dla nowych projektów i prężenie rozwijane. Powstają niemal od podstaw celem łatwiejszego rozwijania kodu, który w przypadku np. poprzednich wersji Entity Framework był bardzo ciężki w utrzymaniu przez twórców. Dodatkowym plusem będzie obsługa innych systemów takich jak Linux czy macOS. MS zdaje się podążać w dobrym kierunku, szkoda jednak że tak późno.
* Zakres pobieranych danych w zależności od roli
* Wyeliminować dziwne metody będące połączeniem async i sync. Lepiej stworzyć normalne async i zadbać o configureawait, by nie tworzyć deadlocków i móc je wywoływać synchronicznie w razie potrzeby.
* Dodać ConfigureAwait w metodach asynchronicznych, gdy nie korzystają z głównego kontekstu
