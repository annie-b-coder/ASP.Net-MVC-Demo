# ASP.Net-MVC-Demo
For testing new different features.

- Custom AuthorizeAttribute

- Custom Authorization


Использование кастомного атрибута, основанного на Роли пользователя в системе. В данном примере демонстрируется в контроллере AccountController путём добавления к методам контроллера. 
После авторизации под пользователем admin (pwd:admin666) можно добавлять новые Роли и Пользователей (в методах Account/Users, Account/Roles). А после авторизации под user1 (pwd:user111) к данным методам доступа нет - происходит редирект на страницу входа.
