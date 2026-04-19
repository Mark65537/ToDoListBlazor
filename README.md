# Сайт списка задач

[![.NET](https://img.shields.io/badge/.NET_8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core_8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)
[![Blazor](https://img.shields.io/badge/Blazor_8.0-512BD4?style=for-the-badge&logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap_5.1.0-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF_Core_8.0.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![Microsoft SQL Server](https://img.shields.io/badge/EF_Core_SqlServer_8.0.0-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://learn.microsoft.com/ef/core/providers/sql-server/)
[![NLog](https://img.shields.io/badge/NLog_5.2.7-4B55C4?style=for-the-badge&logo=nlog&logoColor=white)](https://nlog-project.org/)

![image](https://github.com/Mark65537/ToDoListBlazor/blob/master/Screens/screen1.png)

## Описание

Разработана система управления задачами. Система позволяет редактировать иерархию задач, а также проводить некоторые вычисления по их полям данных.

## Общее описание процесса

Стандартная схема работы с системой выглядит следующим образом:

1. Задачи заносятся в систему (создаются, редактируются, удаляются).
2. К любой задаче могут быть добавлены подзадачи.
3. При удалении задачи, удаление поддерева не требуется.
4. Структура задачи и подзадачи одинакова. Подзадача не может принадлежать более чем
одной задаче. Количество уровней подзадач не ограничено.
5. Поля «Плановая трудоёмкость задачи» и «Фактическое время выполнения» вычисляемые
и складываются из сумм подзадач, входящих в данную задачу, и самой задачи.

Каждая задача, отслеживаемая системой, характеризуется следующим набором атрибутов:

- Наименование задачи
- Описание задачи
- Список исполнителей (простое текстовое поле, ссылочность не нужна)
- Дата регистрации задачи в системе
- Статус задачи: Назначена, Выполняется, Приостановлена, Завершена (число статусов для
системы фиксировано и сами статусы неизменны).
- Плановая трудоёмкость задачи
- Фактическое время выполнения
- Дата завершения
