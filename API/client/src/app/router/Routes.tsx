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
import HomePage from "../../features/home/HomePage";
import RequireAuth from "./RequireAuth";
import Inventory from "../../features/admin/PRStatus";
import PRForm from "../../features/admin/PRForm";
import AssetCatalog from "../../features/ASSET-catalog/AssetCatalog";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [

      // authen routes
      {element: <RequireAuth />, children: [
          { path: "", element: <HomePage /> },
          { path: "pr-catalog", element: <Catalog /> },
          { path: "pr-catalog/:id", element: <PRDetails /> },
          { path: "prform", element: <PRForm cancelEdit={function (): void {
            throw new Error("Function not implemented.");
          } } /> },
          { path: "asset-catalog", element: <AssetCatalog assets={[]} /> },
        ]},

      //admin routes
      {element: <RequireAuth roles={['Approver']} />, children: [
        {path: 'prstatus', element: <Inventory />},
      ]},

      
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
