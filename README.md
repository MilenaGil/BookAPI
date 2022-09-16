# BookAPI

## -AuthenticationController 

- służy do tego by po dostaniu od nas loginu i hasła (UserName & Password) wygenerował nam się token, czyli służy do autentykacji jak sama nazwa mówi.

Wklejamy ten token w miejsce na autoryzacje tokenu i inne kontrollery nam działają bez tego wywala nam błąd 401 Undocumented.

For example (terminated example):

![image](https://user-images.githubusercontent.com/72659265/190627053-ff04b7dd-e803-4e22-a1a2-129807ef4453.png)


## - Wszysko:

Oprócz autoryzacji mam tu też wersjonowanie, które służy do tego by w przyszłości móc bez problemu ulepszać wersję nr 1.

By swaggerze działały poniższe endpointy proszę ustawić Media type na application/json.

## -BooksController 

- ma 2 endpointy GET. 

Ten pierwszy bez "id" w scieżce służy do wyszukiwania w zależności od ustawień nadanych przez nas:
 wszystkich książek/ tylko tylu ile się zmieści na podanej ilości stron o podanym rozmiarze (defaultowo jest to 1dna strona o rozmiarze 10)
/ksiązek o podanym tytule/książek w których opisie bądz nazwie badz id choć częsciowo jest to co wpisaliśmy w searchQuery-pole do wyszukiwania.
![image](https://user-images.githubusercontent.com/72659265/190629117-20b3a9cf-59e3-4959-b3fb-81679b3b3dcd.png)


Ten drugi z "id" w scieżce służy do wyszukiwania książek o podanym id- i możemy wybrać za pomocą pola typu bool czy chcemy by wypisało nam tylko książkę z tym id czy tą książkę i przypisanych do niej bohaterów.

![image](https://user-images.githubusercontent.com/72659265/190626909-5d336c93-13e2-4390-8cf1-ae16f73af165.png)

![image](https://user-images.githubusercontent.com/72659265/190626966-21f3ca98-a3a9-4d22-85a4-59f17dd3a425.png)

## - FilesController 

- ma 1den endpoint GET, dzięki któremu możemy pobrać plik przekazany w projekcie - u mnie to plik typu pdf 

## - HeroesController 

- ma 6 endpointów:

1) GET z bookId - odpowiada za pobranie i wyświetlenie wszystkich bohaterów ksiązki o danym ID
2) POST - odpowiada za dodanie bohatera do ksiązki o podanym ID

![image](https://user-images.githubusercontent.com/72659265/190629263-aa726bbd-e5f4-4ba5-8a06-5295c1b5f270.png)

Możemy w GETie zobaczyć że się dodało:

![image](https://user-images.githubusercontent.com/72659265/190629346-f4749922-6a65-4383-b570-5f89a3904704.png)

3) GET z bookId i heroId - odpowiada za pobranie i wyświetlenie konkretnego bohatera o danym ID z ksiązki o podanym ID
4) PUT - odpowiada za aktualizacje wszystkich pól dotyczących bohatera o podanymID z książki o podanym ID.

![image](https://user-images.githubusercontent.com/72659265/190629837-b28e2b53-ef44-4628-ac7c-0bcd5c7dc62e.png)

![image](https://user-images.githubusercontent.com/72659265/190629879-70fb7b48-d970-4560-beaf-75d2ae851915.png)

![image](https://user-images.githubusercontent.com/72659265/190629943-aef8a031-6912-4a71-ab9a-9766b9b65769.png)

(PATCH próbowałam przed PUTem więc jest wynik z PATCHa w powyższym zdjęciu - zmiany tylko nazwy, na POSTAC)

5) PATCH - odpowiada za aktualizacje tylko wybranych części pól dotyczących bohatera o podanymID z książki o podanym ID.

![image](https://user-images.githubusercontent.com/72659265/190626723-4d8f34bc-34af-433b-8aa2-1b6b7f619711.png)

6) DELETE - odpowiada za usunięcie danego bohatera

![image](https://user-images.githubusercontent.com/72659265/190630180-a4129c2a-6558-4f51-9cef-fc1033ad1500.png)

Nie ma tego z id=8:

![image](https://user-images.githubusercontent.com/72659265/190630266-dae1cf8e-bcda-43c6-884e-3a6eaccd53c8.png)

