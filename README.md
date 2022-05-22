# Broadridge Interview Task

## Details

- Proszę utworzyć nową aplikację webową z wykorzystaniem języka C#, która będzie wyświetlać czas dla zdefiniowanej strefy czasowej, wykorzystując API: http://worldtimeapi.org/

- Nazwa strefy czasowej powinna być definiowana w pliku konfiguracyjnym aplikacji.

- Czas powinien być wyświetlany na stronie webowej generowanej przez aplikację.

Sugerowany stos technologiczny to .NET lub .NET Core + Angular do wyświetlenia strony. Rozwiązanie można też przygotować z zastosowaniem innych, dowolnie wybranych technologii (jednak zastosowanie sugerowanego stosu będzie najwyżej punktowane).

Dodatkowe wymagania (opcjonalnie):

- Jeśli strefa czasowa nie będzie prawidłowo skonfigurowana lub będzie podana nieistniejąca stefa czasowa, należy wyświetlić listę dostępnych stref czasowych.

- Jeśli API będzie niedostępne, należy wyświetlić komunikat o błędzie.

## Jak uruchomić

**UWAGA! Adres API jest zakodowany w pliku `frontend/src/main.ts` na `http://localhost:5000/time/` i w razie chęci skorzystania z innego niż 5000 portu na backendzie, należy go podmienić.**

1. Przy użyciu docker'a (zalecane)

   1. Wystarczy uruchomić `docker-compose up` w głównym folderze repozytorium. Aby przetestować różne strefy czasowe, wystarczy podmienić zawartość zmiennej `ASPNETCORE_TIMEZONE`. Po uruchomieniu aplikacji api dostępne jest pod portem 5000, a frontend pod 8080.

2. Ręcznie
   1. Aby uruchomić frontend należy wpisać `yarn` i `yarn dev` (lub `npm install` i `npm run dev`) w folderze `frontend`.
   2. Aby uruchomić backend należy wpisać `dotnet run` w folderze `backend/BIT.Api`.
