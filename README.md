# Subscription Manager
## Setup Environment
### Front-end
* This is a single-page application powered by [Vue.js](https://vuejs.org)
* This SPA is located in `SubscriptionManager.Web` project, under `app` folder.
* Run `npm install` on first checkout to install all Node modules
* Run `npm run dev` to host this SPA at http://localhost:8088 (configurable via `app/config/index.js`)
* Run `npm run build` to package this application (configurable via `app/config/index.js`)
  * `Index.cshtml` in `Views\SubscriptionManager` folder
  * JavaScript and CSS in `app\dist\static` folder
### Back-end
* Target _.NET Framework 4.5.2_
* All projects except `SubscriptionManager.Web` belong to back-end.
* Design based on CQRS architecture and adopt use case driven approach
* Explore API at [API's root url]/swagger
### Database
* Require [RavenDB](https://ravendb.net) server, version >=3.0 <4.0
* Connection string (configurable via `SubscriptionManager.Services\Web.config`)
  * Server address: http://localhost:8080
  * Database name: `Test`
