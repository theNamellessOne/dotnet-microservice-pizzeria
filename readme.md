Значит так сука шо это шо это за гавно и что оно делает

1. Сервисы

Тут есть 4 сервиса 
1. UserService - отвечает за создание юзеров. ну и за просмотр данных юера/юзеров.
2. CoffeeService - отвечает за создания/удаление продукта коффе / опций для заказа коффе (размер пачки (500гр или сколько, похуй)). При создании либо удалении кофе оно кидает ивент в RabbitMq, на которые потом могут реагировать сабскрайберы (ордерСервис, фаворитСервис). они могут тоже создать/удалить такой кохфе из своей базы (да у них она отдельная ибо нехуй)
3. OrderService - отвечает за заказ кохфе, кидаешь реквестик в него запихиваешь кофе айди из сервиса выше плюс опцию размера. дальше оно кидает само реквест на кофиСервис, чтобы получить инфу о цене кофе, существует ли кофе с таким айди и тому подобное, если всё заебись оно само заполнит оставшиеся данные и создаст заказ. Ну также можно просмотреть все заказы и те кофе, который есть в базе этого сервиса. При запуске оно кидает Grpc запрос на кофиСервис в попытке получить все кофе которые есть там, если каких-то кохфе, которые есть у CoffeeService на данных момент нет в бд OrderService, оно и копирнет в локальную БД (типо на случай если этот сервис упадёт, а в кофиСервисе кто-то создаст новые кофи, чтобы их можно было заказать без проблем). ну и ещё оно подписано на шину ивентов RabbitMq и когда ты в CoffeeService создаёшь новое кохфе оно автоматом добавит его себе в базу
4. FavoriteService - отвечает за добавление кофе юзеру в любимое/избранное. имеет свою базу с 3 таблицами: юзерс-копирует юзеров (как при стартапе при помощи грпс так и подписью на шину ивентов) 2. копирует кофе (также как и юзеров, но дополнительно реагирует на уделение). 3 фаворитес- сохраняет в бд отношение многие ко многим

по функционалу +- всё, а мб и нет (надеюсь в мите я лучше объясню)

дальше по плану запуск
тут смари
запускать в теории можно как локально так и на кубернете
но по факту только на кубернете ибо тут есть зависимость от RabbitMq, который тут (как и в видосе) работает только через кубернетес
то бишь запустить его локально можно только если уже запустил на кубернете, что чутка дебилизм, но уже как есть

для запуска на кубернете сделать надо вот шо:
1. скачать и настроять ебучий докер (на линуксе изи, а вот с виндой у меня когда-то были траблы, хз как оно щас). надо шобы приложуха открывалась и иконки внизу (вроде) горели зелёным
2. также надо будет создать акк на dockerhub и в идеале забилдить все докерИмажи самому, закинуть на докерхаб и заюзать их в файлах в папке K8S (это на самом деле опционально, ведь то, что я там прописал будет работать, но там мой докерхаб привязан)
3. запустить deploy.sh из папки K8S
4. кайфовать (предварительно подождав некоторое время, ибо тут ему иногда тяжко запустится)
5. по ожиданию: оно бывает разным. когда первый раз запускаешь может быть довольно долго ибо ему надо скачать много всего потом легче, но есть нюанс, если ты его убил нахуй (тем шо в некст пункте) и заново запустил через деплой, то ему надо будет тоже дето минуту. оно даже крашнется пару раз ибо там есть зависимость от RabbitMq и сервака с базой данных, только после того как это два пидора нормально запустились оно будет работать
6. ну и потом убить при помощи kill.sh из той же папки

тот опциональный пункт номер 2
шобы его выполнить надо бля:
1. создать акк на докерхаб
2. зайти в папку с каждым сервисом и в терминале написать такие комманды
   1. docker build -t <твой-акк-на-докерХаб>/<названиесервиса> . (эта точка часть команды (обязательная))
   2. docker push <твой-акк-на-докерХаб>/<названиесервиса>
   3. оно будет чтука долго
3. в папке K8S все поля image заменить на твои
   1. пример: было thenamellessones/userserivce:latest стало <твой-акк>/userservice:latest
4. вот теперь вроде всё

(если ты на винде, а ты скорее  всего на винде, то файлам надо поменять расширение из .sh на .bat работать они вроде должны также (я вроде писал их так шоб всегда работали) но если шото пойдёт не так, пиши)

если ты всему этому следовал, и оно даже запустило, то оно всё равно не будет работать через инсомнию (я тебе кину файлы шобы потестировать (если не забуду))

для того чтобы оно работало через инсомнию (тип в видосе её юзал) тебе надо будет добавить в список хостов coffee-shop.me котоырй будет редиректить на 127.0.0.1 я неебу как это делать на винде, но оно вроде было в видосе (если не забуду тут прикреплю)
(не забыл: +- тут https://youtu.be/DgVjEo3OGBI?t=18048)

чтобы запустить "локально":
1. открыть папку сервиса в консоли
2. напечатать dotnet restore
3. напечатать dotnet run 
4. повторить для оставшихся сервисов

тут я куда-то прикреплю файл инсомнии. ты её скачать и импортни в свою. тут уже прописаны всё ендпоинты какие туда можно кидать запросы и как их можно кидать. единственное, что тебе надо будет местами менять айдишники ибо я ебал это делать

вроде как бы и всё по коду я тебе покажу (а может уже и показал) в мите, комменты местами есть, да и большая часть кода с понятными названиями (можешь угадать, что делает метод с названием GetAllUsers? правильно, удаляет нахуй весь проект (щютка (хихи хаха)))
виш ю лак и с тебя пивандепала