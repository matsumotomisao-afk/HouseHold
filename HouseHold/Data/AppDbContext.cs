
using Microsoft.EntityFrameworkCore;
using HouseHold.Models;
namespace HouseHold.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<IncomeClass> IncomeClasses { get; set; } = null!;
        public DbSet<IncomeType> IncomeTypes { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public DbSet<SubjectName> SubjectNames { get; set; } = null!;
        public DbSet<MonthlyBudget> MonthlyBudgets { get; set; } = null!;

        // 科目ImageDB登録用メソッド
        public void SeedImagesFromWwwroot() // wwwroot/Images フォルダ内のPNGファイルを SubjectName テーブルに登録するメソッド
        {
            try
            {
                // wwwroot/Images の絶対パスを取得
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Console.WriteLine($"[ERROR] 画像フォルダが存在しません: {imagesPath}");
                    return;
                }
                // PNGファイル一覧を取得
                var pngFiles = Directory.GetFiles(imagesPath, "*.png", SearchOption.TopDirectoryOnly);
                foreach (var filePath in pngFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    // DBに保存する相対パス（例: "images/sample.png"）
                    var relativePath = Path.Combine("Images", fileName).Replace("\\", "/");
                    // 重複チェック
                    if (!SubjectNames.Any(s => s.ImageUrl == relativePath))
                    {
                        var subject = new SubjectName
                        {
                            //SubjectNameIdは自動生成されるため、指定しない
                            CourseName = Path.GetFileNameWithoutExtension(fileName), // ファイル名をName列に設定（拡張子なし）
                            ImageUrl = relativePath
                        };
                        SubjectNames.Add(subject);  // DBに追加
                    }
                }
                SaveChanges();                    // 変更を保存
                Console.WriteLine("[INFO] 画像登録が完了しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 登録処理中にエラー: {ex.Message}");
            }
        }
        // データベースに初期データを挿入するメソッド
        public void SeedPaymentTypeData(AppDbContext db)
        {
            if (db.PaymentTypes.Any()) return; // データが既に存在する場合はスキップ

            // 起動時に挿入する初期データ
            var paymentTypes = new List<PaymentType>
          {
              //PaymentTypeIdは自動生成されるため、指定しない
              new() {TypeName = "現金" },
              new() {TypeName = "モバイル決済" },
              new() {TypeName = "クレジットカード" },
              new() {TypeName = "電子マネー" },
              new() {TypeName = "銀行自動引き落とし"}
          };
            db.PaymentTypes.AddRange(paymentTypes);// データベースに保存
            db.SaveChanges();                    // 変更を保存
        }
        //PaymentMethods」テーブルにデータを挿入する
        public void SeedPaymentMethods(AppDbContext db)  // データベースに初期データを挿入するメソッド
        {
            if (db.PaymentMethods.Any()) return; // データが既に存在する場合はスキップ
                                                 // 起動時に挿入する初期データ
            var paymentMethods = new List<PaymentMethod>
            {
                //TypeMemoIdは自動生成されるため、指定しない
                new() {MethodName = "なし" , PaymentTypeId = 1},
                new() {MethodName = "PayPay", PaymentTypeId = 2},
                new() {MethodName = "郵貯Pay", PaymentTypeId = 2},
                new() {MethodName = "郵貯カード", PaymentTypeId = 3},
                new() {MethodName = "＊", PaymentTypeId = 4},
                new() {MethodName = "ゆうちょ銀行", PaymentTypeId = 5},
                new() {MethodName = "三井住友銀行", PaymentTypeId = 5}
            };
            db.PaymentMethods.AddRange(paymentMethods);// データベースに保存
            db.SaveChanges();                    // 変更を保存
        }
        //IncomClassテーブルにデータを挿入する
        public void SeedIncomeClassData(AppDbContext db)
        {
            if (db.IncomeClasses.Any()) return; // データが既に存在する場合はスキップ

            // 起動時に挿入する初期データ
            var incomClasses = new List<IncomeClass>
            {
                //IncomClassIdは自動生成されるため、指定しない
                new() {IncomeName = "給与"},
                new() {IncomeName = "事業所得"},
                new() {IncomeName = "不動産所得"},
                new() {IncomeName = "雑所得"},
                new() {IncomeName = "配当所得"},
                new() {IncomeName = "一時所得"},
                new() {IncomeName = "譲渡所得"},
                new() {IncomeName = "退職所得"},
                new() {IncomeName = "山林所得"},
                new() {IncomeName = "利子所得"}

            };
            db.IncomeClasses.AddRange(incomClasses);// データベースに保存
            db.SaveChanges();                    // 変更を保存
        }
        //IncomTypeテーブルにデータを挿入する
        public void SeedIncomeTypeData(AppDbContext db)
        {
            if (db.IncomeTypes.Any()) return; // データが既に存在する場合はスキップ
            // 起動時に挿入する初期データ
            var incomTypes = new List<IncomeType>
            {
                //IncomTypeIdは自動生成されるため、指定しない
                new() {TypeName = "給料", IncomeClassId = 1},
                new() {TypeName = "賞与", IncomeClassId = 1},
                new() {TypeName = "役員報酬", IncomeClassId = 1},
                new() {TypeName = "自営業の売上", IncomeClassId = 2},
                new() {TypeName = "フリーランス報酬", IncomeClassId = 2},
                new() {TypeName = "家賃収入", IncomeClassId = 3},
                new() {TypeName = "地代", IncomeClassId = 3},
                new() {TypeName = "駐車場収入", IncomeClassId = 3},
                new() {TypeName = "公的年金", IncomeClassId = 4},
                new() {TypeName = "副業の収入（単発）", IncomeClassId = 4},
                new() {TypeName = "暗号資産", IncomeClassId = 4},
                new() {TypeName = "株、配当金", IncomeClassId = 5},
                new() {TypeName = "投資信託分配金", IncomeClassId = 5},
                new() {TypeName = "保険の満期払い戻し金", IncomeClassId = 6},
                new() {TypeName = "懸賞金", IncomeClassId = 6},
                new() {TypeName = "株式の売却", IncomeClassId = 7},
                new() {TypeName = "不動産の売却", IncomeClassId = 7},
                new() {TypeName = "ゴルフ会員権の売却", IncomeClassId = 7},
                new() {TypeName = "退職金", IncomeClassId = 8},
                new() {TypeName = "iDeCoの一時金", IncomeClassId = 8},
                new() {TypeName = "確定拠出年金の一時金", IncomeClassId = 8},
                new() {TypeName = "山林の譲渡", IncomeClassId = 9},
                new() {TypeName = "山林の伐採", IncomeClassId = 9},
                new() {TypeName = "預金の利子", IncomeClassId = 10},
                new() {TypeName = "公社債の利子", IncomeClassId = 10}
            };
            db.IncomeTypes.AddRange(incomTypes);// データベースに保存
            db.SaveChanges();                    // 変更を保存
        }
    }
}
