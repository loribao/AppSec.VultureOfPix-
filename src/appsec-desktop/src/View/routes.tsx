import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Projeto from "./pages/Projects";
import MyProfile from "./pages/MyProfile";

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

