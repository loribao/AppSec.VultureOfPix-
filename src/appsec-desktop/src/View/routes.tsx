import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import App from "./App";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import MenuDock from "./pages/MenuDock";

export const Router = () => {

    return (
        <BrowserRouter>
            <Routes>
                <Route path="/Dashboard" element={<Dashboard />} />
                <Route path="/Login" element={<App><Login /></App>} />
                <Route path="/Dock" element={<MenuDock />} />
                <Route path="/" element={<App><Login /></App>} />
            </Routes>
        </BrowserRouter>)
}
