using System;
using System.Collections.Generic;

// IPerson arayüzü
public interface IPerson
{
    bool GirişYap(string kullanıcıAdı, string şifre);
}

// Person sınıfı (Temel sınıf)
public class Kişi : IPerson
{
    public string Ad { get; set; }
    public int Yaş { get; set; }
    public string KimlikNo { get; set; }

    public Kişi(string ad, int yaş, string kimlikNo)
    {
        Ad = ad;
        Yaş = yaş;
        KimlikNo = kimlikNo;
    }

    // Genel bilgi gösterimi
    public virtual string BilgiGöster()
    {
        return $"Ad: {Ad}, Yaş: {Yaş}, Kimlik No: {KimlikNo}";
    }

    // Giriş yapma metodu
    public bool GirişYap(string kullanıcıAdı, string şifre)
    {
        Console.WriteLine($"Kullanıcı {kullanıcıAdı} olarak giriş yapılıyor...");
        return true; // Gerçek bir uygulamada doğrulama yapılır.
    }
}

// Öğrenci sınıfı
public class Öğrenci : Kişi
{
    public string ÖğrenciNo { get; set; }
    public List<Ders> KayıtlıDersler { get; set; }

    public Öğrenci(string ad, int yaş, string kimlikNo, string öğrenciNo)
        : base(ad, yaş, kimlikNo)
    {
        ÖğrenciNo = öğrenciNo;
        KayıtlıDersler = new List<Ders>();
    }

    // Ders kaydı
    public void DersKaydıYap(Ders ders)
    {
        if (!KayıtlıDersler.Contains(ders))
        {
            KayıtlıDersler.Add(ders);
            ders.ÖğrenciEkle(this);
            Console.WriteLine($"{Ad} {ders.Ad} dersine kaydoldu.");
        }
        else
        {
            Console.WriteLine($"{Ad} zaten {ders.Ad} dersine kaydolmuş.");
        }
    }

    // Öğrenciye özel bilgi gösterimi
    public override string BilgiGöster()
    {
        var dersAdları = string.Join(", ", KayıtlıDersler.ConvertAll(ders => ders.Ad));
        return $"{base.BilgiGöster()}, Öğrenci No: {ÖğrenciNo}, Kayıtlı Dersler: {dersAdları}";
    }
}

// Öğretim Görevlisi sınıfı
public class ÖğretimGörevlisi : Kişi
{
    public string AkademikNo { get; set; }
    public List<Ders> VerdiğiDersler { get; set; }

    public ÖğretimGörevlisi(string ad, int yaş, string kimlikNo, string akademikNo)
        : base(ad, yaş, kimlikNo)
    {
        AkademikNo = akademikNo;
        VerdiğiDersler = new List<Ders>();
    }

    // Ders atama
    public void DersEkle(Ders ders)
    {
        if (!VerdiğiDersler.Contains(ders))
        {
            VerdiğiDersler.Add(ders);
            ders.ÖğretimGörevlisiAtama(this);
            Console.WriteLine($"{Ad} {ders.Ad} dersinin öğretim görevlisi olarak atandı.");
        }
        else
        {
            Console.WriteLine($"{Ad} zaten {ders.Ad} dersini veriyor.");
        }
    }

    // Öğretim görevlisine özel bilgi gösterimi
    public override string BilgiGöster()
    {
        var dersAdları = string.Join(", ", VerdiğiDersler.ConvertAll(ders => ders.Ad));
        return $"{base.BilgiGöster()}, Akademik No: {AkademikNo}, Verdiği Dersler: {dersAdları}";
    }
}

// Ders sınıfı
public class Ders
{
    public string Ad { get; set; }
    public int Kredi { get; set; }
    public ÖğretimGörevlisi ÖğretimGörevlisi { get; set; }
    public List<Öğrenci> Öğrenciler { get; set; }

    public Ders(string ad, int kredi)
    {
        Ad = ad;
        Kredi = kredi;
        Öğrenciler = new List<Öğrenci>();
    }

    // Öğretim görevlisi atama
    public void ÖğretimGörevlisiAtama(ÖğretimGörevlisi öğretimGörevlisi)
    {
        ÖğretimGörevlisi = öğretimGörevlisi;
    }

    // Öğrenci ekleme
    public void ÖğrenciEkle(Öğrenci öğrenci)
    {
        if (!Öğrenciler.Contains(öğrenci))
        {
            Öğrenciler.Add(öğrenci);
        }
    }

    // Ders bilgilerini gösterme (kredi dahil)
    public string BilgiGöster()
    {
        var öğrenciAdları = string.Join(", ", Öğrenciler.ConvertAll(öğrenci => öğrenci.Ad));
        return $"Ders: {Ad}, Kredi: {Kredi}, Öğretim Görevlisi: {(ÖğretimGörevlisi != null ? ÖğretimGörevlisi.Ad : "Öğretim Görevlisi Yok")}, Öğrenciler: {öğrenciAdları}";
    }
}

// Ana Program
public class Program
{
    public static void Main(string[] args)
    {
        // Öğretim görevlisi oluşturuluyor
        ÖğretimGörevlisi öğretimGörevlisi1 = new ÖğretimGörevlisi("Esat Durmaz", 40, "171121", "EGT123");
        ÖğretimGörevlisi öğretimGörevlisi2 = new ÖğretimGörevlisi("Göktuğ Sarıçayır", 38, "200823", "GST456");

        // Öğrenciler oluşturuluyor
        Öğrenci öğrenci1 = new Öğrenci("Öykü Akdeniz", 21, "10293", "S1234");
        Öğrenci öğrenci2 = new Öğrenci("Dilara Hatipoğlu", 22, "28374", "S5678");

        // Dersler oluşturuluyor
        Ders ders1 = new Ders("Matematik", 3);
        Ders ders2 = new Ders("Fizik", 4);

        // Öğretim görevlisi derse atanıyor
        öğretimGörevlisi1.DersEkle(ders1);
        öğretimGörevlisi2.DersEkle(ders2);

        // Öğrenciler derse kaydoluyor
        öğrenci1.DersKaydıYap(ders1);
        öğrenci2.DersKaydıYap(ders1);
        öğrenci2.DersKaydıYap(ders2);

        // Ders bilgileri yazdırılıyor
        Console.WriteLine(ders1.BilgiGöster());
        Console.WriteLine(ders2.BilgiGöster());

        // Öğrenci bilgileri yazdırılıyor
        Console.WriteLine(öğrenci1.BilgiGöster());
        Console.WriteLine(öğrenci2.BilgiGöster());

        // Öğretim görevlisi bilgileri yazdırılıyor
        Console.WriteLine(öğretimGörevlisi1.BilgiGöster());
        Console.WriteLine(öğretimGörevlisi2.BilgiGöster());
    }
}


