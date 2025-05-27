
# 🐳 Bamboo Card - Docker Setup & Features

## 🚀 Setup Instructions

### 1. Download & Extract
1. Download the ZIP file.
2. Right-click on it and select **"Extract All"**.  
   Example path: `{yourZipFolder}\bamboo-card-master`.

### 2. Start Docker
1. Launch **Docker Desktop**.

### 3. Build & Run
1. Navigate to `{yourZipFolder}\bamboo-card-master\bamboo-card-master`.
2. Type `cmd` in the address bar to open the terminal in this folder.
3. Run the following command:
   ```bash
   docker-compose up --build
   ```
4. This will create containers for the application and a SQL Server instance.

### 4. Verify Running Containers
Run:
```bash
docker ps
```
Ensure both app and DB containers are running.

### 5. Install nopCommerce
1. Open [http://localhost:8080](http://localhost:8080) in your browser.
2. Use the following database info:
   - **Server name**: `nopcommerce_mssql_server`
   - **Database name**: (Any name you prefer)
   - **User**: `sa`
   - **Password**: `nopCommerce_db_password`

3. Complete the installation.

### 6. Restart Application in Detached Mode
1. Press `Ctrl+C` in the current terminal to stop the running containers.
2. Start them in the background:
   ```bash
   docker-compose up -d
   ```
3. Visit [http://localhost:8080](http://localhost:8080) again to access the site.

---

## 🏷️ Discount Configuration

1. Log in to the admin portal.
2. Navigate to [Admin Discount Config](http://localhost:8080/Admin/CustomDiscount/Configure).
3. Set the following:
   - **Enable**: `true`
   - **Discount Percentage**: `10`
4. Click **Save**.

---

## 🛒 Apply Discount

1. Complete **3 or more orders**.
2. Add any product to the cart.
3. A **10% discount** will be applied to the order total (visible above the Total section).

---

## 🎁 Gift Message Feature

### Add a Gift Message
1. Visit [Cart Page](http://localhost:8080/cart).
2. Scroll below the Gift Cards section.
3. Enter a message and click **"Add Gift Message"**.
4. Complete the order.

### View in Order Details
1. Go to [Order Details](http://localhost:8080/orderdetails) and select your order.
2. Scroll below Order Total to see the **Gift Message**.

### View in Admin Portal
1. Navigate to [Admin Order Edit](http://localhost:8080/Admin/Order/Edit).
2. Click **View** on the desired order.
3. In the Info card, below Order Status, the **Gift Message** is displayed.

---

## 📬 Retrieve Order Details via Postman

1. Open Postman and **import** the following collection:
   - `"nopCommerce Custom Order API.postman_collection.json"`  
     → located at:  
     `Plugins\Nop.Plugin.DiscountRules.CustomDiscounts\PostmanCollection`
2. Run **"Generate Token"** API to authenticate.
3. Run **"Order details"** API to retrieve order info.

### 🛠️ Troubleshooting

If Postman requests aren't working:

- Import the environment file:  
  `"Local.postman_environment.json"` from:  
  `Plugins\Nop.Plugin.DiscountRules.CustomDiscounts\PostmanCollection`.

---

## 📁 Project Structure Overview

```
bamboo-card-master/
├── bamboo-card-master/
│   ├── docker-compose.yml
│   ├── README.md
│   └── Plugins/
│       └── Nop.Plugin.DiscountRules.CustomDiscounts/
│           └── PostmanCollection/
│               ├── nopCommerce Custom Order API.postman_collection.json
│               └── Local.postman_environment.json
```

---
