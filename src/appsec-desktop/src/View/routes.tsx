import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Projeto from "./pages/Projects";
import MyProfile from "./pages/MyProfile";

export const router = createBrowserRouter([
    {
        path: "/projectPage",
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
]);

