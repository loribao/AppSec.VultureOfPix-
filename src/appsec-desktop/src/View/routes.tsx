import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Login from "./pages/Login";
import Projeto from "./pages/Projects";
import MyProfile from "./pages/MyProfile";
import PageClimp from "./components/PageClimp";
import Modal from "./components/Modal";
import { Header } from "./components/Header";
import Footer from "./components/Footer";
import  MenuDock  from "./components/MenuDock";

export const router = createBrowserRouter([
    {
        path: "/",
        element: (
            <App>
                <MyProfile />
            </App>
        ),
    },
    {
        path: "/projectPage",
        element: (
            <App>
                <Projeto />
            </App>
        ),
    },
]);
