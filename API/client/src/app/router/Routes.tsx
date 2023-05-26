import { createBrowserRouter } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import App from "../layout/App";
import Catalog from "../../features/catalog/Catalog";
import AboutPage from "../../features/about/AboutPage";
import PRDetails from "../../features/catalog/PRDetails";
import ContactPage from "../../features/contact/ContactPage";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <Catalog />},
            // {path: 'catalog', element: <Catalog />},
            {path: 'catalog/:id', element: <PRDetails />},
            {path: 'about', element: <AboutPage />},
            {path: 'contact', element: <ContactPage />}
        ]
        }
])