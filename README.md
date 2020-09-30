# TaskManager

**I'm not offering any license** | **Nie udzialam żadnej licencji**

Strona wdrożona na Azure: https://taskmanagerweb20200911175909.azurewebsites.net/

Role w systemie:
* Admin
* Serwisant
* Klient

Rola jest nadawana użytkownikowi m.in. w zależności od tego do jakiego kontrahenta jest przypisany. Klienci widzą zadania przypisane jedynie do danego kontrahenta. Serwisanci widzą wszystkie zadania. Klienci i Serwisanci mają różne zakresy statusów do przebijania. Serwisant może wybrać kontrahenta i przedstawiciela przy tworzeniu zgłoszenia, a klient nie może. Serwisant może zmieniać wykonawcę zgłoszenia i ustawiać budżet (którego klient nie widzi), a klient może ustawiać priorytet. Admin może na przykład zmieniać i usuwać wszystko co zechce. Widoczność przycisków do przebijania statusów jest zależna od roli i stanu zgłoszenia, na przykład jako "Do odbioru" przebijać może jedynie osoba przypisana do danego zgłoszenia jako wykonująca. Oczywiście po stronie API jest walidacja, która nie pozwoli na przebicie na określony status, jeśli użytkownik lub zadanie nie spełniają określonych wymogów. To samo jest z dostępem do danych, klient może pobrać jedynie kontrahenta do którego jest przypisany i pracowników jedynie przypisanych do tego kontrahenta. Serwisanci i Admini mogą pobrać wszystkich itd. To samo tyczy się edycji, nie ma możliwości by klient zmienił budżet czy opis zadania.

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

_Na portalu są także założone konta testowe_:

```csharp
var employee = new Employee
{
    ContrahentId = 2,
    FirstName = "Paweł",
    LastName = "Klient",
};

CreateUser(manager, "klient@gmail.com", "hasloos@1Z", "Klient", "Klientów", employee);


employee = new Employee
{
    ContrahentId = 1,
    FirstName = "Paweł",
    LastName = "Operator",
};

CreateUser(manager, "serwisant@gmail.com", "hasloos@1Z2", "Serwisant", "Serwisantów", employee);
```

> The https://github.com/robots.txt file of GitHub allows the indexing of the blobs in the 'master' branch, but restricts all other branches. So if you don't have a 'master' 
> branch, Google is not supposed to index your pages.

## Wstęp

Oprócz wytwarzania oprogramowania, innym elementem działalności firm informatycznych jest obsługa zgłoszeń serwisowych w których najczęściej zawarte są problemy klientów z oprogramowaniem, prośby o dodanie nowych funkcjonalności do oprogramowania czy pytania typowo biznesowe. W toku działalności firmy informatycznej jej zarząd często podejmuje decyzję o usprawnieniu obsługi zgłoszeń poprzez wdrożenie portalu zgłoszeniowego który będzie narzędziem komunikacji klientów, jak i pracowników firmy (programistów, zarządu i osób odpowiedzialnych za obsługę klienta). Celem takiego systemu jest także wspomaganie zarządzania zadaniami pracowników w firmie informatycznej.

## Technologia

Single Page Application utworzone z wykorzystaniem knockout.js, HTML, CSS, Bootstrap 4.5, JavaScript z wykorzystaniem najnowszej ECMY, jQuery, inne pomniejsze biblioteki. Jest to aplikacja ASP.NET MVC 5 korzystająca z Entity Framework i komunikująca się z REST API powstałym w ASP.NET WebApi.

## Funkcjonalności

* Dodawanie zadań 
* Podgląd zadań 
* Wyszukiwanie zadań 
* Dodawanie komentarzy do zadań
* Usuwanie komentarzy (tylko swoich)
* Edycja zadań
* Przebijanie statusów w toku realizacji
* Generowanie CSV z wyfiltrowanych tasków
* Rejestracja, logowanie, zmiana hasła, potwierdzanie konta

## Cechy

*   Walidacja po stronie klienta, a także po stronie serwera. Rezultat walidacji po stronie serwera także jest widoczny dla użytkownika (np. ogólna informacja czy informacje dotyczące poszczególnych propert)
*   Filtrowanie po stronie klienta (np. priorytet i typ zadania), a w niektórych przypadkach po stronie  serwera (np. data utworzenia zadania)
*   Wykorzystuje wzorzec MVC, Rest Repository, UOW, Strategy, Command, Chain of Responsibility, IoC, Factory itd.
*   API jest wersjonowane, w response raportowana jest wersja. Domyślną wersją jest 1.1. Możliwe jest określenie innej jako query string lub nagłówek.
*	Obsłużona została edycja równoległa w przyjazny dla użytkownika sposób, np. przy edycji widoczne są wartości jakie ustawił inny użytkownik po pobraniu danych przez danego użytkownika.
*	APi posiada liczne funkcjonalności i operacje dostępne tylko dla admina, np. szyfrowanie i aktualizacja konfiguracji, edycja wszystkich danych, pobieranie wielokryterialne danych w różnej formie. Nie ma jednak skrajności typu RPC.
*	Aplikacja składa się z warstwy biznesowej, dostępu do danych i warstwy webowej. Planowana jest także warstwa WPF czy Windows.
*	Web API korzysta z repozytoriów, które działają w oparciu o ustaloną strategię dostępu do danych opartą o role i cechy użytkowników i obiektów systemu. Każdy użytkownik może edytować określone dane i musi spełnić dane warunki by na przykład utworzyć zgłoszenie czy usunąć komentarz. Sprawia to, że z API będą mogły korzystać także inne aplikacje, np. okienkowe bez zbytniego przywiązywania uwagi do logiki biznesowej i obsługi poświadczeń
*	Dostęp do API jest autoryzowany tokenem. Na stronie jest zaimplementowana logika tworzenia Bearer Token dla danego użytkownika i odświeżania tokenów.
*	API jest aktualnie wykorzystywane jedynie na potrzeby strony, jednak w planach jest otwarcie dostępu dla innych typów aplikacji – okienkowych czy mobilnych.
*	Zadania i komentarze są tworzone z wykorzystaniem edytora tekstu.
*	Aplikacja tworzy logi z wykorzystaniem Log4Net. Możliwy jest podgląd logów, np. z pzoziomu Cloud Explorer w Visual Studio – logi trafiają do pliku w podfolderze logs aplikacji
*	Możliwe jest aktywowanie różnych typów logów
*	Możliwe jest włączenie logów wszystkich zapytań i danych o ich wykonaniu. Dotyczy to zapytań wywoływanych po stronie EF, jak i tradycyjnych.
*	Możliwe jest włączenie logów do bazy danych
*	Aplikacja na bieżącym etapie jest w pełni funkcjonalna, jednak wymaga rozwoju
*	Aplikacja zbiera dane o logujących się użytkownikach - dane przekazywane przez przeglądarkę, a także uzyskane z zewnętrznego API na podstawie IP użytkownika.
*	Aplikacja obsługuje różne strefy czasowe, każdy użytkownik widzi odpowiedni dla jego kultury format daty dostosowany do jego strefy czasowej. Obsługiwane są dwa języki interfejsu – angielski i polski.
* Wraz z każdym requestem strona wysyła do API nagłówek z timezone offsetem (od UTC). Offset jes także przechowywany w cookie.
*	API zwraca daty w formie UTC ISO, a przyjmuje w każdej formie. Jeśli jest to format ISO, to dostosowuje je do UTC o ile nie jest to Zulu. Jeśli jest to format inny niż ISO, to następuje parsowanie daty z wykorzystaniem kultury użytkownika i korekta o timezone offset wysłany przez  użytkownika API poprzez nagłówek lub plik cookie. Jeśli timezone offset nie został wysłany, to data jest traktowana jako UTC
*	W bazie danych są przechowywane jedynie daty UTC.
*	Użytkownik może filtrować zadanie z wykorzystaniem formatu daty odpowiedniego dla jego kultury i strefy czasowej. Po stronie REST API jego data jest odpowiednio dostosowywana do UTC i w ten sposób są pobierane odpowiednie dane.
*	Automatyczne tworzenie numerów ticketów z wykorzystaniem triggera.
*	Aplikacja wykorzystuje bibliotekę programistyczną utworzoną w toku mojej programistycznej przygody. Biblioteka jest na bieżącym etapie dość obszerna i stopniowo rozwijana. Panuje w niej lekki chaos, więc warto jakoś go ujarzmić. Biblioteki programistycznej postaowiłem nie udostępniać w formie publicznego repozytorium.
*	Aplikacja korzysta z AutoMappera. Wykorzystuje przede wszystkim projekcję IQueryable, dzięki czemu w prosty sposób tworzone są wydajne zapytania z wielu tabel bez zbędnego i żmudnego kodowania wyrażeń LinQ.
*	Aplikacja jest wyposażona w klienta email
*	Aplikacja korzysta z podmodułów (przede wszystkim z poziomu GitHub)
*	Aplikacja wyświetla błędy w przyjazny dla użytkownika sposób
*	Aplikacja loguje i obsługuje wszystkie błędy w aplikacji, w niektórych miejscach lokalnie, a w innych globalnie
*	Aplikacja wykorzystuje IoC z pomocą AutoFaca. Dzięki licznym interfejsom, aplikacja jest testowalna i w szybki sposób możliwe jest zastępowanie implementacji czy mockowanie (np. z wykorzystaniem moq) bez zmian w wielu miejscach,
*	Tłumaczenie nie tyczy się tylko interfejsu, ale także generycznych danych pochodzących z bazy danych czy komunikatów o błędach.
*	Strona pod kątem interfejsu niemal w całości obsługuje dwa języki: polski i angielski.

## Planowane zmiany

*	Bardziej modułowa budowa po stronie JS, np. z wykorzystaniem require.js czy modułów z nowej Ecmy. Będą moduły dostępu do danych, router, bootstrapper itd.
*   Korzystać z Promises, deffered execution, np. by mieć wpływ na kolejność wykonywania operacji asynchroncznych.
*   Nie pobierać co chwilę lookupów, tylko przechowywać je w jednym, wspólnym dla partiali miejscu. Zapisywać stan strony, filtrów itd. Zrobić użytek z local storage czy Amplifier.js.
*   Dodać cache po stronie API
*   Dodać czyszczenie filtrów przy bluru
*   Uniemożliwić wielokrotne wysyłanie tych samych requestów przed zakończenie poprzednich - command dla buttonów, dezaktywacja do momenu wykonania operacji
*	Dodanie change trackingu po stronie knockout i wysyłanie żądań do API tylko w przypadku edycji jakichkolwiek danych przez użytkownika. Ostrzeganie przed wyjściem z zadania przed zapisaniem danych.
*	Całkowite wyeliminowanie alertów i dodanie zamiast tego dialogów współpracujących z knockout i korzystających z jQuery UI. Utworzę custom bindings.
*	Implementacja Event Stormingu i CQRS
*	Otworzenie dostępu do API dla innych klientów, a nie tylko aplikacji webowej
*   Być może wydzielenie serwera autoryzacyjnego i API do osobnych projektów, póli, by były wyizolowane i niezależnie działały.
*	Zgodnie z punktem poprzednim, dodanie weryfikacji client id, request url, a także danych logowania wysyłanych na url pozwalające na utworzenie tokenu ("/Token").
*	Dodanie bazy wiedzy i przypisywania wpisów do zadań
*	Dodanie modułu planowania i harmonogramowania zadań
*	Dodanie modułu raportów
*	Utworzenie aplikacji WPF czerpiącej z praktyk programowania asynchronicznego z wykorzystaniem Caliburn.Micro, nowych funkcji takich jak AsyncIEnumerable. Utworzenie aplikacji w Android Studio. Obie aplikacje będą wersjami portalu kolejno okienkowymi i mobilnymi i będą korzystały z utworzonego Web API. Być może zmiast Android Studio pomyśleć nad Xamarin. W końcu Javę ogarniam dość pobieżnie w przeciwieństwie do .NET.
*	Dodanie pobierania skryptów i stylów z CDN z fallbackiem do serwera celem zmniejszenia wykorzystania zasobów chmury.
*	Przepisanie aplikacji na Angular. Knockout jest dość przestarzałą, ubogą i już niewspieraną, nierozwijaną biblioteką. Rozważyć dość modne ostatnio Vue.js. Rozważyć też     React, który zdaje się stopniowo wypierać Angulara.
*	Czerpać ze wzorców programowania reaktywnego
*	Wdrożyć komunikator, pomyśleć nad wykorzystaniem bazy NoSQL – ORM nadal jako Entity Framework.
*	Wyeliminować dość niewygodne dla użytkownika odświeżanie UI, przede wszystkim CKEditor. Zbadać flow routingu.
*	Nieco popracować nad czystością kodu i wdrożyć SRP przede wszystkim jeśli chodzi o przebijanie statusów. W innych miejscach kod jest nawet czysty i wygląda ok.
*	Pokrycie aplikacji testami jednostkowymi i integracyjnymi. Obecnie ilość testów jest bardzo znikoma. Dość sporo testów jest związanych z biblioteką programistyczną (PDCory), ale z samym TaskManagerem już tak średnio. Przed aplikacja zdaje się być nieprzewidywalna. Dodatkowo utworzyć testy jednostkowe po stronie JavaScript, np. z wykorzystaniem QUnit i MockJson
*	Wykorzystywać strefy czasowe zamiast timezone offsetów. Dodać możliwość ustawienia strefy czasowej przez użytkownika w szczegółach profilu.
*	Dodanie obsługi avatarów użytkowników
*	Dodanie obsługi załączników w komentarzach i ticketach
*	Dodanie możliwości edycji większej ilości danych w ticketach, np. opisu czy tematu.
*	Zrobić tak, by był brak możliwości edycji ticketów po ich zakończeniu
*	Dodać więcej statusów ticketów, np. „Do zatwierdzenia”, „Zatwierdzone”, „Do wypuszczenia”, „Wymagany wpis do bazy wiedzy”, „Wymagane dane techniczne”.
*	Dodanie do ticketów sekcji z danymi technicznymi do wykorzystania przez serwisantów
*	Dodanie możliwość tworzenia kontrahentów
*	Dodanie weryfikacji pracowników przez kontrahentów – sam kod licencyjny i NIP to za mało, by uzyskać dostęp do portalu
*	Przejść na ASP.NET Core  i Entity Framework Core. Są zalecane przez Microsoft dla nowych projektów i prężenie rozwijane. Powstają niemal od podstaw celem łatwiejszego rozwijania kodu, który w przypadku np. poprzednich wersji Entity Framework był bardzo ciężki w utrzymaniu przez twórców. Dodatkowym plusem będzie obsługa innych systemów takich jak Linux czy macOS. MS zdaje się podążać w dobrym kierunku, szkoda jednak że tak późno.
* Zakres pobieranych danych w zależności od roli
* Wyeliminować dziwne metody będące połączeniem async i sync. Lepiej stworzyć normalne async i zadbać o configureawait, by nie tworzyć deadlocków i móc je wywoływać synchronicznie w razie potrzeby.
* Dodać ConfigureAwait w metodach asynchronicznych, gdy nie korzystają z głównego kontekstu - aspekt wydajnościowy.
* Dodać moduł powiadomień o zdarzeniach związanych z ticketami, dodatkowo powiadomienia mailowe.
* Potworzyć funkcje tabelaryczne na potrzeby raportów i procedury, np. by oznaczyć wszystie powiadomienia jako przeczytane.
