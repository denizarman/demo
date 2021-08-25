# .Net Core Microservis Mimarisi Kodlama Standartları ve Kod Örnekleri Demo Projesi

Solution, microservis mimarisi örnek yapısı, kod örnekleri ve kodlama standartlarını tarifler.

## Proje Hiyerarşisi <img src="./documentation_resources/projects.png" align="right" height="120" /> <br>

Bu demo projesinde microservis, 4 katmandan oluşmaktadır. İhtiyaç halinde, bu yaklaşım esnetilebilir, ama çoğu senaryo için 4 soyutlaştırma katmanı ihtiyacı giderecektir.

### Demo.Api

Backend microservis'in sunu katmanı *(Web API projesi)* ve .Net Core Solution'ının bootstraper projesidir.
 
Bootstrapper projesi olduğu için, .NetCore'un IoC Container yapısı bu proje dahilinde bulunacaktır. Bu nedenle, **diğer tüm projelerin referansı bu projeye eklenmelidir.** Başka hiçbir proje bu, projeden referans almayacağı için, aşağıda bulunan proje klasörleri içerisinde detaylandırılacak olan sınıflar, **bu proje** için kullanılacak yardımcı sınıflar olacaktır.

Kullanıcı Arayüzü, bu katmanla entegre olacağı için, kullanıcı arayüz iş kuralları için yazılacak cross cutting concerns, validasyon, mapping gibi işlemler bu katmana adreslenmelidir. 

<img src="./documentation_resources/demoapi.png" align="left" height="200" />

Bazı açıklamalar;

*Aspect Oriented Programming (AoP), uygulama iş kurallarına dahil olmayan, altyapısal geliştirmelerin, uygulama iş kurallarından soyutlanması. Proje içerisinde bu sorumlulukta olan 2 farklı klasör olacak, middleware ve attributes. Bu klasörlerin farkları aşağıda detaylandırılacak.*  

#### Proje dizinleri ve açıklamaları

##### Attributes

Proje içerisinde kullanılan 2 AoP katmanından birisidir. Bu klasör içerisinde .Net'in Attribute sınıfından türeyen sınıflardan oluşacaktır. Attribute'ler Http pipeline'da *(Tüm Requestler için çalışacak akış = middleware)* **kullanılmayacak** olan AoP iş kurallarıdır. Üzerine konduğu EndPoint için, WebAPI katmanındaki kodun önüne*(before)* ve sonuna*(after)* kontrol eklemeyi mümkün kılar. 

Örnekler;

CacheAttribute; 
Before: Üzerine konduğu endpoint'e gelen isteğin yanıtı, CacheProvider içerisinde var ise, Cache'den sonucu alır, ve kullanıcıya yanıt döner. 
WebAPI: Eğer CacheProvider üzerinde veri yok ise, EndPoint katmanına düşer. Endpoint ilgili datayı veritabanından çeker.
After: İstemciye yanıt dönmeden önce, dönülecek yanıtı Cache Provider'a iletir. 

AuthorizeAttribute;
Bir EndPoint'i belirli bir rol tarafından erişilebilir yapmak için, gelen http isteğindeki Token'ın içerisinde rol listesinde, Attribute üzerinde belirlenen rolün olup olmadığını kontrol eder.

##### Controllers

##### Dtos

##### Filters

##### Helpers

##### HostedServices

##### Middlewares

Kullanıcı Arayüz projesinin arayüzde ihtiyaç duyduğu Nesne Modellerini DTO soneki ile belirtiyoruz. 

### Demo.Core

### Demo.Data

### Demo.Service
