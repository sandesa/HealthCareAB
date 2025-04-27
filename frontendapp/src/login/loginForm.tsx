import React, { useState } from 'react';
import Cookies from 'js-cookie';
import api from '../api';
import './loginForm.css';

interface LoginResponse {
    userId?: number;
    accessToken?: string;
    expires?: string;
    message?: string;
    isConnectedToService: boolean;
    isLoginSuccessful: boolean;
}

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [loginData, setLoginData] = useState<LoginResponse | null>(null);
    const [message, setMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        setLoading(true);
        setError('');

        try {
            const response = await api.post<LoginResponse>('api/login', { email, password });
            if (response.data.isLoginSuccessful) {
                Cookies.set('auth_token', response.data.accessToken!.toString(), { expires: 1 });
                Cookies.set('user_id', response.data.userId!.toString(), { expires: 1 });
                setMessage(response.data.message!);
                setLoginData(response.data);
                
            } else {
                setError(response.data.message!);
            }
        } catch (error: any) {
            setError('Failed to login. Please check your credentials or try again later.');
        } finally {
            setLoading(false);
        }
    };
    return (
        <div className="login-form">
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="text"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>

                {error && <p className="error">{error}</p>}

                <div>
                    <button type="submit" disabled={loading}>
                        {loading ? 'Logging in...' : 'Login'}
                    </button>
                </div>
            </form>

            {loginData && loginData.isLoginSuccessful && (
                <div className="login-success">
                    <p>{message}</p>
                </div>
            )}
        </div>
    );
}

export default LoginForm;