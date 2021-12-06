# TomasekProducts
Výsledná řešení :
A) TomasekRestApi je výsledek mého snažení úkolu, který jsem dostal
B) Tournament je WinForm xaml aplikace, kterou jsem napsal asi 4 roky zpět. Napadlo mě, že by mohla posloužit jako příklad, že trošku umím programovat. Její popis najdete níže
Je to můj kod napsaný pro soukrome ucely, takže to můžu vystavit

Popis vývoje testovaci REST aplikace
0. REST api jsem jednou zkoušel udělat v rámci domácího úkolu pro pohovor ve firmě Deloitte. Jak se později ukázalo, nakonec chtěli něco jiného. :)
1. Ve čtvrtek jsem dostal zadání , ale dostal jsem se k realizaci až v nedělil 6.12
2. Prvním čim jsem začal je, že jsem si na Azure udělal DB, a v ní tabulku Product, kterou jsem naplnil nějakými testovacími daty. Jak se později ukázalo, toto jsem dělat neměl
Jako SEED operaci jsem chtěl použít klasickou možnost vegenerovat DB jako sql script i s daty.
3. Instalace VS2022, .Net Core 6, přidávání balíčků pro .NET EF,...
4. Zhlédnutí videa https://www.youtube.com/watch?v=fmvcAzHpsk8, jen základní body a pak jsou koukal do jeho GIT repository
5. Použití Scaffold-DbContext pro vygenerování modelu
6. Napsání Repository a funkcí v ní
7. Vytvoření Controlleru, zatím nepoužití Dto objektu
8. Rozchození prvního HTTP GETu
9. Configurace a testování verzovaní
10. Realizace Seed oprace, zde vidím, že se to mělo udělat naopak, nejprve model a pak DB, ale mě se prostě líbí ten starší způsob více. 
Enable-Migrations, Update-Database, Add-Migration - zkoušel jsem si s tím hrát, výsledek je ve složce Migration, která je ale vlastně k ničemu
Nakonec ale pro Seed nepoužito, místo toho je třeba volat
dotnet run seeddata z hlavního programu
11. Dopsání Dto objektu, konfigurace Mapperu před ProductProfile
12. První commit do GITu, ano, měl přijít dříve, ale aspoň teď píšu tento postup
13. Konzultace s bráchou o tom jak by to udělal on, do té doby jsem nechtěl aby mi cokoliv radil, chtěl jsem se to naučit sám a pochopit
14. Rozdělení na vrstvy, BL, zde jsem jen přesunul Repository, ale asi by to mělo ještě vypadat jinak.
15. Přidání testů, MockRepository, zkoušení testů
16. Přidání CryptoHelperu, slouží jen proto, aby nebyl čitelný connectionstring, o úrovni bezpečnosti lze  polemizovat
17. Finalní commit-push
18. Connection-string asi netřeba měnit, mělo by to jet odkudkoliv

Tournament Popis
1. Již asi 10 let hraji badminton, a čas od času někdo pořádal tzv losované turnaje. Jenže na to není pořádný programek, tak jsem si řekl, že by to nebyla špatná zábava napsat.
Tehdá jsem myslel, že xaml formy jsou in, nuž dopadlo to jak to dopadlo. Dnes bych to udělal jako web, a do mobilní appky dal možnost zadávaní výsleků hráčum,... viz Hura Liga
2. Co appka tedy umí 
Debly
a) Představte si 100 hráčů, kteří si chtějí zahrát 5-10 zápasů, tak aby vždy měli jiného protihráče i spoluhráče. 
Ne vždy to jde, od jistého počtu kol, se zejména protihráči nutně musí začít opakovat. Dále je tu přídavné kriterium, že spolu hraje pokud to jde muž a žena
b) Představte si 10 můžu a 10 žen, a každý muž chcce hrát přávě jednou s každou z žen, a nechce aby potkával stejné soupeře. 
Toto jde, ale nalézt správnou kombinaci zápasů není vůbec jednoduché
c) Předdefinované - zde mám některé předdefinované RoundRobin pro sudý i lichý počet hráčů

Další kriteria -  nasazení na kurty, tak aby nehráli stále na stejném kurtu. V halách bývají lepší-horší kurty

Některé zde popsané problémy vyžadují implementaci brutal force s určitou heuristikou, připadně nejaký backtracking mechanismus pro výběr zápasů pro dané kolo,...
Také díky performance testům jsem odhalil,kde daná aplikace má bottle neck

Pro zájemce můžu představit nejzajimavější místa kodu, ukázat program za běhu,...

Není to žádná hitparáda, čistě kod, kterým jsem se bavil a měl řešit problém
Občas mi někdo i napíše, abych mu poslal kod k tomuto prográmku, že chce dělat losovaný turnaj pro tenis, badminton








