import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '../index.css'
import Navbar from '../navbar/navbar.tsx'
import LoginForm from './loginForm.tsx'

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)

createRoot(document.getElementById('login-form')!).render(
    <StrictMode>
        <LoginForm />
    </StrictMode>
)