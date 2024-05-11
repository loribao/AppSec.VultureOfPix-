import React, { useState } from 'react';
import loginimg from '../assets/img/Vulture_Art.svg'
import '../assets/css/index.css'
import {container} from "tsyringe";
import ILoginCommand from '../../Domain/interfaces/ICommands/ILoginCommand';
export default function Index() {
    const [isPasswordVisible, setPasswordVisible] = useState(false);
    const [login_txt,setLogin_txt] = useState('');
    const [Pass_txt,setPass_txt] = useState('');
    const [checked, setChecked] = useState(false);
    const togglePasswordVisibility = () => {
        setPasswordVisible(!isPasswordVisible);
    };

    const handleLogin = (e:  React.ChangeEvent<HTMLInputElement>) => {
        setLogin_txt(e.target.value)
    }
    const handlePassword = (e:  React.ChangeEvent<HTMLInputElement>) => {
        setPass_txt(e.target.value)
    }

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        let res = await container.resolve<ILoginCommand>('ILoginCommand').Handler({username: login_txt, password: Pass_txt})
        console.log(login_txt,Pass_txt, "status: ", res.status)
        if(res.status === "success"){
            window.location.href = "/projectPage"
        }
    }
    return (
        <main className="mainLoginContainer">
            <section className="leftSection">
                <div className="rectangle"></div>
                <div className="personagemLogin-container">
                    <img
                        className="imageLogin personagemLogin .vultureSvg"
                        src={loginimg}
                        alt="Personagem Página Login"
                    />
                </div>
                <div className="rectangle"></div>
            </section>
            <section className="rightSection">
                <section className="rightSectionUm">
                    <h1 className="titleUm">Bem-vindo ao AppSec!👋🏻</h1>
                    <p className="paragrafo paragrafoPrincipal">
                        Faça login em sua!
                    </p>
                    <form className="formLoginContainer" onSubmit={handleSubmit}>
                        <input
                            className="inputLogin"
                            type="text"
                            placeholder="LogIn"
                            value={login_txt}
                            onChange={handleLogin}
                            autoComplete={checked ? "name" : "off"}
                        />
                        <div className="passwordToggleContainer">
                            <input
                                className="inputLogin"
                                value={Pass_txt}
                                onChange={handlePassword}
                                type={isPasswordVisible ? "text" : "password"}
                                id="senhaInput"
                                placeholder="Senha"
                                autoComplete={checked ? "current-password" : "off"}
                            />
                            <span
                                className="passwordToggle"
                                onClick={togglePasswordVisibility}
                            ></span>
                        </div>
                        <div className="divLoginUm">
                            <div className="checkboxLoginContainer">
                                <input
                                    className="checkboxLogin"
                                    type="checkbox"
                                    id="lembrarCheckbox"
                                    onChange={(e) =>  setChecked(!checked)}
                                    checked={checked}
                                />
                                <label
                                    className="custom-checkbox"
                                    htmlFor="lembrarCheckbox"
                                ></label>
                                <p className="paragrafo">Lembrar</p>
                            </div>
                        </div>
                        <button type='submit' className="buttonLogin" >Log In</button>
                    </form>
                </section>
            </section>
        </main>
    )
}
