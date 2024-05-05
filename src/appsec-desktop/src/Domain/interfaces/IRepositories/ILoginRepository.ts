export default interface ILoginRepository {
    login: (username: string, password: string) => Promise<string>;
    logout: () => Promise<boolean>;
}
