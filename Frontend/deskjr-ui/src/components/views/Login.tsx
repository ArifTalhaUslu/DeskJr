import React, { useEffect, useState } from "react";
import Input from "../CommonComponents/Input";
import Button from "../CommonComponents/Button";
import Card from "../CommonComponents/Card";
import { useDispatch, useSelector } from "react-redux";
import { LoginForm } from "../../types/user";
import { AppState } from "../../store";
import { login } from "../../store/actions/userActions";
import { useNavigate } from 'react-router-dom';

const Login: React.FC = () => {
    
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const { data, loading, error } = useSelector((state: AppState) => state.user);

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        const formData: LoginForm = { email, password };
        dispatch(login(formData));
    };

    useEffect(() => {
        if (data.email) {
            console.log("Login Başarılı");
        }
    }, [data.email]);

    

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <Card title="Login">
                        <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <Input
                                    type="text"
                                    className="form-control"
                                    id="email"
                                    value={email}
                                    onChange={(e)=> setEmail(e.target.value)}
                                    required
                                    placeholder="Enter your email"
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <Input
                                    type="password"
                                    className="form-control"
                                    id="password"
                                    value={password}
                                    onChange={(e)=> setPassword(e.target.value)}
                                    required
                                    placeholder="Enter your password"
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
