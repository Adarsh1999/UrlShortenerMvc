# 🔗 URL Shortener MVC

A modern, beginner-friendly, and feature-rich TinyURL-style application built with ASP.NET Core MVC and PostgreSQL. Easily shorten, share, and track your links with expiration and analytics!

---

## 🚀 Features

- ✂️ **Shorten long URLs** with a single click
- ⏳ **Set expiration dates** for your short links
- 📊 **Track click analytics** (IP, user agent, referrer, time)
- 🗂️ **Sidebar with all your created short URLs** for easy access
- 🖥️ **Modern, responsive UI** (Bootstrap 5, FontAwesome)
- 🔒 **Secure** and production-ready foundation

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 8+)
- **Database:** PostgreSQL
- **Frontend:** Bootstrap 5, FontAwesome
- **ORM:** Entity Framework Core
- **Validation:** FluentValidation
- **Containerization:** Docker (for PostgreSQL)

---

## 📸 Screenshots

| Home / Landing Page | Create Short URL | Stats & Analytics |
|--------------------|-----------------|------------------|
| ![Home](https://img.icons8.com/fluency/96/link.png) | ![Create](https://img.icons8.com/color/48/add-link.png) | ![Stats](https://img.icons8.com/color/48/combo-chart--v1.png) |

---

## 📝 Getting Started

### 1. **Clone the Repository**
```bash
git clone https://github.com/your-username/url-shortener-mvc.git
cd url-shortener-mvc
```

### 2. **Configure the Database**
- Make sure you have [Docker](https://www.docker.com/) installed.
- Start PostgreSQL with Docker:

```bash
docker-compose up -d
```

- Update your `appsettings.Development.json` with the correct connection string if needed:
```json
{
  "ConnectionStrings": {
    "UrlDb": "Host=localhost;Port=5432;Database=url_shortener;Username=postgres;Password=secret"
  }
}
```

### 3. **Apply Migrations**
```bash
dotnet ef database update
```

### 4. **Run the Application**
```bash
dotnet run
```

- Visit [http://localhost:5265](http://localhost:5265) in your browser.

---

## ✨ Usage

1. **Create a Short URL:** Paste your long URL, optionally set an expiry, and click "Shorten URL".
2. **Copy & Share:** Use the copy button to grab your new short link.
3. **Track Analytics:** Click any short URL in the sidebar to view its stats (clicks, referrers, etc).
4. **Manage Links:** All your created links are always visible in the sidebar for easy access.

---

## 🧑‍💻 For Developers

- **.NET 8+** required
- All dependencies managed via NuGet
- Code is clean, commented, and beginner-friendly
- Easily extensible for authentication, custom domains, etc.

---

## 🐳 Docker Compose (PostgreSQL)

A `docker-compose.yml` is included for easy local database setup:
```bash
docker-compose up -d
```

---

## 🛡️ Security & Best Practices
- User input is validated and sanitized
- No secrets or sensitive files are committed (see `.gitignore`)
- Ready for deployment (just add your own authentication, HTTPS, etc)

---

## 🙏 Credits & Inspiration
- [TinyURL](https://tinyurl.com/)
- [ASP.NET Core Docs](https://learn.microsoft.com/aspnet/core)
- [Icons8](https://icons8.com/) for icons

---

## 💡 Ideas for Extension
- User authentication (track links per user)
- Custom short codes
- QR code generation
- API for programmatic shortening
- Admin dashboard

---

## 📬 Feedback & Contributions

Pull requests, issues, and suggestions are welcome! If you found this project helpful, please ⭐ star the repo and share it with others.

---

> Made with ❤️ using ASP.NET Core MVC, Bootstrap, and PostgreSQL. 