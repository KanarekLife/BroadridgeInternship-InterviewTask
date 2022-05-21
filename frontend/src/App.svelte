<script lang="ts">
	import {onMount} from "svelte";

	export let apiUrl: string;

	let time: Date | null = null;
	let apiError: {
		title: string;
		type: string;
		detail: string;
		ValidTimezones?: string[];
		CurrentTimezone?: string;
	} | null = null;
	let error: Error | null = null;
	let interval;

	function pad(num: number): string {
		return num.toString().padStart(2, '0')
	}

	$: formattedTime = time !== null ? `${pad(time.getHours())}:${pad(time.getMinutes())}:${pad(time.getSeconds())}` : null;
	$: isNetworkError = error !== null && error.message === 'NetworkError when attempting to fetch resource.';

	function setupInterval() {
		if (interval) return;
		interval = setInterval(() => {
			if (time != null) {
				time = new Date(time.getTime() + 1000);
			}
		}, 1000);
	}

	onMount(() => {
		getCurrentTimeFromApi()
			.then(date => {
				time = date;
				setupInterval();
			}).catch(err => {
				console.log(err);
				error = err;
			})
		return () => clearInterval(interval);
	})

	async function getCurrentTimeFromApi(): Promise<Date> {
		time = null;
		apiError = null;
		error = null;

		const res = await fetch(apiUrl);

		if (res.ok) {
			const dto = await res.json();
			const x = dto.time.split(':');
			return new Date(0,0,0,parseInt(x[0]),parseInt(x[1]),parseInt(x[2]));
		}else {
			apiError = await res.json();
			console.log(apiError)
		}
	}

	async function refresh(): Promise<void> {
		await getCurrentTimeFromApi()
			.then(date => {
				time = date;
				setupInterval();
			}).catch(err => {
			error = err;
		});
	}
</script>

<main class="main">
	<div class="container">
		{#if apiError}
			<div class="notification is-danger">
				<h1><b>{apiError.title}</b></h1>
				<p>{apiError.detail}</p>
				<p>Current Timezone: {apiError.CurrentTimezone}</p>
				<hr />
				<h2>Available timezones:</h2>
				<ul class="timezones_list">
					{#each apiError.ValidTimezones as timezone}
						<li>{timezone}</li>
					{/each}
				</ul>
			</div>
		{:else if isNetworkError}
			<div class="notification is-danger network-error">
				<h1><b>Couldn't connect to the API</b></h1>
				<button class="button" on:click={refresh}>Synchronize with the API</button>
			</div>
		{:else if error}
			<div class="notification is-danger">
				{error.message}
			</div>
		{:else if !time}
			<progress class="progress is-primary" max="100">15%</progress>
		{:else }
			<article class="message is-primary">
				<div class="message-body">
					<p>Current time: {formattedTime}</p>
					<button class="button" on:click={refresh}>Synchronize with the API</button>
				</div>
			</article>
		{/if}
	</div>
</main>
<footer>
	
</footer>

<style>
	.main {
		height: 100vh;
		width: 100vw;
		display: grid;
		place-items: center;
	}
	.timezones_list {
		max-height: 600px;
		overflow-y: scroll;
	}
	.network-error {
		display: flex;
		flex-direction: column;
		align-items: center;
	}
</style>
