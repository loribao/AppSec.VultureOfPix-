import "reflect-metadata";
import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { router } from "./View/routes";
import Registry from "./Data/bootstraps/Registry";
import createTray from "./Data/contexts/Tray"
const setup = async () => {
    const _registre = new Registry();
    if(_registre){
        console.log("Registry created")
    }
    if(await createTray()){
        console.log("Tray created")
    }
}
setup();
ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(

  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);
