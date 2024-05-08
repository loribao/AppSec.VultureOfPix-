import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Login from "./pages/Login";
import Projeto from "./pages/Projects";
import MyProfile from "./pages/MyProfile"

export const router = createBrowserRouter([
    {
        path: "/",
        element: (
            <App>
                <Projeto />
            </App>
        ),
    },
    {
        path: "/projectPage",
        element: (
            <App>
                <MyProfile />
            </App>
        ),
    },
]);
