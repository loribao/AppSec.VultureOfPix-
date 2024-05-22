import type { IApiRequest } from "../../Domain/interfaces/IContexts/IContext";
import { inject, injectable } from "tsyringe";
import ILoginRepository from "../../Domain/interfaces/IRepositories/ILoginRepository";
import type IContext from "../../Domain/interfaces/IContexts/IContext";

@injectable()
class LoginRepository implements ILoginRepository {
    constructor(@inject("IContext") private driver: IContext) {}



    login = async (username: string, password: string): Promise<string> => {
        let query = {"query": `
        mutation {
            login(logIn: { userLogin: "${username}", password: "${password}" }) {
              token
            }
          }
        `};

        let url = new URL("/graphql","https://localhost:5081/graphql/")
        let request: IApiRequest<any> = {
            url: url.toString(),
            body: query,
            headers: {
                "Content-Type": "application/json"
            }
        }

        let { status, data } = await this.driver.Api.post<string, {data: {login: {token: string}};}>(request);
        if (status >= 200 && status < 400) {
            return data.data.login.token;
        }
        else {
            throw new Error("Error on authenticate");
        }
    }
    logout = async (): Promise<boolean> => {
        throw new Error("Method not implemented.");
    }
}

export default LoginRepository;
