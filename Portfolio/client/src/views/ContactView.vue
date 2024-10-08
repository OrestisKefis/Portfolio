<template>
    <PageTitle :title="'Contact'"></PageTitle>

    <div class="text-white container d-flex flex-column align-items-center mt-5">
        <!-- <div class="display-3 mb-5">
            Reach out to
            <span class="text-prim">{{ '<' }}me{{ '/>' }}</span>
        </div> -->

        <form class="bg-dark-faded d-flex flex-column gap-4 p-4 rounded shadow" @submit="onFormSubmit">
            <!-- <div class="d-flex gap-4 justify-content-between">
                <FormInput :id="'inpt-firstName'" type="text" :label-text="'First Name'" :flex="'vertical'" :validators="firstNameValidators"
                    v-model="firstName" />
                <FormInput :id="'inpt-lastName'" type="text" :label-text="'Last Name'" :flex="'vertical'" :validators="lastNameValidators"
                    v-model="lastName" />
            </div> -->

            <div class="d-flex gap-4 justify-content-between">
                <FormInput :id="'inpt-email'" type="text" placeholder="Email" :flex="'vertical'"
                    :validators="emailValidators" v-model="email" />
                <FormInput :id="'inpt-subject'" type="text" placeholder="Subject" :flex="'vertical'"
                    :validators="subjectValidators" v-model="subject" />
            </div>

            <div class="d-flex flex-column">
                <!-- <label for="inpt-message">Message</label> -->
                <textarea id="inpt-message" class="form-control" placeholder="Message" rows="7" v-model="message"
                    style="border: 1px solid rgb(124, 121, 121);"></textarea>
            </div>

            <div>
                <Button type="submit" class="float-end">
                    Submit
                </Button>
            </div>
        </form>
    </div>
</template>

<script setup lang="ts">
import { inject, onMounted, ref, type Ref } from 'vue';
import { useFormStore } from '@/stores/formStore';
import type { Validators } from '@/types/formInput';
import { send } from '@/services/contactMeService';
import type { ContactMeRequest } from '@/types/contactMe';

import FormInput from '@/components/FormInput.vue';
import Button from '@/components/Button.vue';
import PageTitle from '@/components/PageTitle.vue';

const store = useFormStore()

const toast: Ref<Components.ToastMessage> = inject('toast')

const firstName = ref<string>('')
const lastName = ref<string>('')
const email = ref<string>('')
const subject = ref<string>('')
const message = ref<string>('')

const firstNameValidators: Validators[] = ['notEmpty', 'inRange', 'isOnlyAlphabetical']
const lastNameValidators: Validators[] = ['notEmpty', 'inRange', 'isOnlyAlphabetical']
const emailValidators: Validators[] = ['notEmpty', 'isEmail']
const subjectValidators: Validators[] = ['notEmpty']

const onFormSubmit = async (event: Event) => {
    store.hasErrors = store.formInputs.some(i => i.errors.length > 0)
    if (store.hasErrors) {
        event.preventDefault()
        return
    }
    const request: ContactMeRequest = {
        email: email.value,
        firstName: firstName.value,
        lastName: lastName.value,
        subject: subject.value,
        message: message.value
    }
    await send(request)
        .then(_ => { })
        .catch(err => {
            toast.value.show(err.response.data.detail, 'error', 4000)
        })
}

onMounted(() => store.hasErrors = false)
</script>