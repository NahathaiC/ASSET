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

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <HomePage />},
            {path: 'catalog', element: <Catalog />},
            {path: 'catalog/:id', element: <PRDetails />},
            {path: 'about', element: <AboutPage />},
            {path: 'contact', element: <ContactPage />},
            {path: 'login', element: <Login />},
            {path: 'register', element: <Register />},
            {path: 'server-error', element: <ServerError />},
            {path: 'not-found', element: <NotFound/>},
            {path: '*', element: <Navigate replace to='/not-found' />}
        ]
        }
])