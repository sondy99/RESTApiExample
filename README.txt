REST API TEST

For this test was not allow use any framework/library apart from .NET

To test using postman / ARC (or any rest client) is important to add the header 

Authorization  Basic c29jaWFscG9pbnQ6dGVzdA== 
content-type   application/json

(take a look on the image ExampleConfigRequest)

Absolute score [PUT]
URL
http://localhost:55324/api/user/AbsoluteScore

Body 
{ "user": 2, "total": 1000 }

Relative score [PUT]
URL
http://localhost:55324/api/user/RelativeScore

Body
{ "user": 5, "score": "-150" }

Absolute ranking [GET]
URL
http://localhost:55324/api/game/top3

Relative Ranking [GET]
URL
http://localhost:55324/api/game/at100/2 


Special considerations

* In the security part in a real solution I would use an oauth server to manage the credentials or something similar, but I just put hardcode an user and passwprd.
* In the business layer I would use an automaper framework.
* For the logger I would use a logging framework like Nlog or something similar.
* In the Unit test I should "mock" some things, but without a Mock framework is complicated.
