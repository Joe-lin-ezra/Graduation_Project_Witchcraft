Player是一個empty物件，以下簡稱P
PlayerModel是一個玩家模型，以下簡稱PM
Player(子物件的那個、VR的那個)，以下簡稱VP

連線時，如果是本地玩家，PM的模型會隱藏，使本地完機看不到自己的模型，
之後就跟普通時候操作VP一樣，P會負責將VP的位置跟新給PM，
PM再將自己的位置使用Mirror函式，同步在遠端玩家的場景中