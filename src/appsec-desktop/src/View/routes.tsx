import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Projeto from "./pages/Projects";
import MyProfile from "./pages/MyProfile";
import Login from "./pages/Login";
import Home from "./pages/Home";

export const router = createBrowserRouter([
    {
        path: "/login",
        element: (
            <App>
                <Login />
            </App>
        ),
    },
    {
        path: "/myprofile",
        element: (
            <App>
                <MyProfile />
            </App>
        ),
    },
    {
        path: "/",
        element: (
            <App>
                <Projeto />
            </App>
        ),
    },

    {
        path: "/home",
        element: (
            <App>
                <Home />
            </App>
        ),
    },
    {
        path: "/",
        element: (
            <App>
                <Login />
            </App>
        ),
    },
]);


