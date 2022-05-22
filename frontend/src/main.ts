import App from './App.svelte';

const app = new App({
	target: document.body,
	props: {
		apiUrl: 'https://localhost:5001/time/'
	}
});

export default app;
