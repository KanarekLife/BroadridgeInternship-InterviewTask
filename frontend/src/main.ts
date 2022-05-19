import App from './App.svelte';

const app = new App({
	target: document.body,
	props: {
		apiUrl: 'https://localhost:7210/time/'
	}
});

export default app;
