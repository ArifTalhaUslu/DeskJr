import React, { useState } from "react";
import Input from "../CommonComponents/Input";
import Button from "../CommonComponents/Button";
import Card from "../CommonComponents/Card";
import LoginService from "../../services/LoginService";

// Yapıyı kullanmak için Kullanıcı adı: GUID ID ve password :"kullanıcı adı" consoleda bilgiler döner

const Login: React.FC = () => {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            const response = await LoginService.login(username, password);
            console.log(response);
        } catch {
            console.log(
                "ID ve ya Şifre başarısız.. UserName: GUID ID password: Kullanıcı Adı"
            );
        }
    };

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <Card title="Login">
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="username">Username</label>
                                <Input
                                    type="text"
                                    className="form-control"
                                    id="username"
                                    value={username}
                                    onChange={setUsername}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <Input
                                    type="password"
                                    className="form-control"
                                    id="password"
                                    value={password}
                                    onChange={setPassword}
                                    required
                                />
                            </div>
                            <Button type="submit" text="Login" />
                        </form>
                    </Card>
                </div>
            </div>
        </div>
    );
};

export default Login;
