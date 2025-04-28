import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import '../index.css'
import './newAppointment.css'
import AppointmentForm from './newAppointment.tsx'
import Navbar from '../navbar/navbar.tsx'

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)

createRoot(document.getElementById('appointment-form')!).render(
    <StrictMode>
        <AppointmentForm />
    </StrictMode>,
)

