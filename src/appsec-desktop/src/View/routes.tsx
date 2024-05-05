import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import Login from "./pages/Login";
import Projeto from "./pages/Projetos";

export const router = createBrowserRouter([
    {
      path: "/",
      element: <App><Login /></App>,
    },
    {
        path: "/projectPage",
        element: <App><Projeto /></App>,
      },
  ]);
