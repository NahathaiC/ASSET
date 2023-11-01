import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import Catalog from "../../features/catalog/Catalog";
import AboutPage from "../../features/about/AboutPage";
import PRDetails from "../../features/catalog/PRDetails";
import ContactPage from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import RequireAuth from "./RequireAuth";
import Inventory from "../../features/admin/PRStatus";
import PRForm from "../../features/admin/PRForm";
import AssetCatalog from "../../features/ASSET-catalog/AssetCatalog";
import AssetDetails from "../../features/ASSET-catalog/AssetDetails";
import PRreport from "../../features/admin/PR-report";
import PurchaseMN from "../../features/home/PurchaseMN";
import UserUpdate from "../../features/admin/UserUpdate";
import HomePage from "../layout/HomePage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [

      // authen routes
      {element: <RequireAuth />, children: [
          { path: "pr-mn", element: <PurchaseMN /> },
          { path: "pr-catalog", element: <Catalog /> },
          { path: "pr-catalog/:id", element: <PRDetails /> },
          { path: "prform", element: <PRForm cancelEdit={function (): void {
            throw new Error("Function not implemented.");
          } } /> },
          { path: "asset-catalog", element: <AssetCatalog /> },
          { path: "asset-catalog/:id", element: <AssetDetails /> },
          
        ]},

      //admin routes
      {element: <RequireAuth roles={['Approver']} />, children: [
        {path: 'prstatus', element: <Inventory />},
        {path: 'pr-report', element: <PRreport />},
      ]},

      {element: <RequireAuth roles={['Admin']} />, children: [
        {path: 'user-mn', element: <UserUpdate />},
      ]},

      { path: "", element: <HomePage /> },
      { path: "about", element: <AboutPage /> },
      { path: "contact", element: <ContactPage /> },
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },
      { path: "server-error", element: <ServerError /> },
      { path: "not-found", element: <NotFound /> },
      { path: "*", element: <Navigate replace to="/not-found" /> },
    ],
  },
]);
