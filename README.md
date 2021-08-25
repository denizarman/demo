# .Net Core Microservis Mimarisi Kodlama Standartları ve Kod Örnekleri Demo Projesi

Solution, microservis mimarisi örnek yapısı, kod örnekleri ve kodlama standartlarını tarifler.

## Proje Hiyerarşisi <img src="./documentation_resources/projects.png" align="right" height="120" /> <br>

### Demo.Api

Backend microservis'in sunu katmanı *(Web API projesi)* ve .Net Core Solution'ının bootstraper projesidir. 
Bootstrapper projesi olduğu için, .NetCore'un IoC Container yapısı bu proje dahilinde bulunacaktır. Bu nedenle, **diğer tüm projelerin referansı bu projeye eklenmelidir.**  

UI projeler, bu katmanla entegre olacakları için, UI iş kuralları için yazılacak her iş kuralı, validasyon, mapping gibi işlemler bu katmana adreslenmelidir. 

### Demo.Core

### Demo.Data

### Demo.Service
