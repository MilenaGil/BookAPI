# BookAPI

## -AuthenticationController 

- służy do tego by po dostaniu od nas loginu i hasła (UserName & Password) wygenerował nam się token, czyli służy do autentykacji jak sama nazwa mówi.

Wklejamy ten token w miejsce na autoryzacje tokenu i inne kontrollery nam działają bez tego wywala nam błąd 401 Undocumented.

## - Wszysko:

Oprócz autoryzacji mam tu też wersjonowanie, które służy do tego by w przyszłości móc bez problemu ulepszać wersję nr 1.

By swaggerze działały poniższe endpointy proszę ustawić Media type na application/json.

## -BooksController 

- ma 2 endpointy GET. 

Ten pierwszy bez "id" w scieżce służy do wyszukiwania w zależności od ustawień nadanych przez nas:
 wszystkich książek/ tylko tylu ile się zmieści na podanej ilości stron o podanym rozmiarze (defaultowo jest to 1dna strona o rozmiarze 10)
/ksiązek o podanym tytule/książek w których opisie bądz nazwie badz id choć częsciowo jest to co wpisaliśmy w searchQuery-pole do wyszukiwania.

Ten drugi z "id" w scieżce służy do wyszukiwania książek o podanym id- i możemy wybrać za pomocą pola typu bool czy chcemy by wypisało nam tylko książkę z tym id czy tą książkę i przypisanych do niej bohaterów.

## 

- FilesController - ma 1den endpoint GET, dzięki któremu możemy pobrać plik przekazany w projekcie - u mnie to plik typu pdf 

## 

- HeroesController - ma 6 endpointów:

1) GET z bookId - odpowiada za pobranie i wyświetlenie wszystkich bohaterów ksiązki o danym ID
2) POST - odpowiada za dodanie bohatera do ksiązki o podanym ID
3) GET z bookId i heroID - odpowiada za pobranie i wyświetlenie konkretnego bohatera o danym ID z ksiązki o podanym ID
4) PUT - odpowiada za aktualizacje wszystkich pól dotyczących bohatera o podanymID z książki o podanym ID.
5) PATCH - odpowiada za aktualizacje tylko wybranych części pól dotyczących bohatera o podanymID z książki o podanym ID.
6) DELETE - odpowiada za usunięcie danego bohatera
