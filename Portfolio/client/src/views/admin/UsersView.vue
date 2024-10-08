<template>
    <div class="container mt-5">
        <div class="fw-bold fs-4 mb-5 text-light">
            Users
        </div>

        <table class="table shadow">
            <thead>
                <tr>
                    <th class="bg-tert py-4 text-light">Email</th>
                    <th class="bg-tert py-4 text-light">Role</th>
                    <th class="bg-tert py-4 text-light"></th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="pagination" v-for="user in  pagination.page">
                    <td class="align-middle bg-dark text-light">{{ user.userName }}</td>
                    <td class="align-middle bg-dark text-light">{{ userRole(user.role) }}</td>
                    <td class="align-middle bg-dark text-light">
                        <div class="d-flex gap-3">
                            <button class="fa fa-edit p-0 bg-dark text-light border-0"
                                @click.prevent="onEdit(user)"></button>
                            <button class="fa fa-trash border-0 bg-transparent text-danger"
                                @click.prevent="onDelete(user)"></button>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>

        <Modal ref="editModal" :title="'Edit User'">
            <div class="d-flex gap-4 align-items-center p-4">
                <div>
                    Role:
                </div>

                <select class="form-control bg-text-prim" v-model="currentRole">
                    <option v-for="role in roles" :key="role.value" :value="role.value">
                        {{ role.key }}
                    </option>
                </select>
            </div>

            <div class="d-flex justify-content-center mb-3">
                <Button @click.prevent="onConfirmEdit" :isDisabled="isDifferentRole">
                    Submit
                </Button>
            </div>
        </Modal>

        <Modal ref="deleteModal" :title="'Delete User'">
            <div class="m-auto mt-4 d-flex flex-column gap-4 p-3">
                <div class="fs-3 mb-3" style="max-width: 20ch;">
                    Are you sure you want to delete current user
                </div>

                <div class="d-flex justify-content-evenly">
                    <div>
                        <OutlineButton @click="deleteModal.hide">
                            Cancel
                        </OutlineButton>
                    </div>

                    <div>
                        <Button @click="onConfirmDelete">
                            Confirm
                        </Button>
                    </div>
                </div>
            </div>
        </Modal>

        <Pagination v-if="users.length > 0" :items="users" :pageSize="8" ref="pagination" />

        <div class="d-flex gap-4 float-end">
            <div>
                <OutlineButton class="fa fa-arrow-left" :isDisabled="pagination?.hasPreviousPage"
                    @click="pagination?.previous" />
            </div>

            <div>
                <OutlineButton class="fa fa-arrow-right" :isDisabled="pagination?.hasNextPage" @click="pagination?.next" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { deleteUser, getUsers, updateUser } from '@/services/adminService';
import { type User, userRole, type UserRole, getAllRoles, type Role } from '@/types/user';
import { computed, inject, onMounted, ref, type Ref } from 'vue';

import Modal from '@/components/Modal.vue';
import Pagination from '@/components/Pagination.vue';
import Button from '@/components/Button.vue';
import OutlineButton from '@/components/OutlineButton.vue';

//TODO: Maybe add a boolean value to the users to make sure they are active users
const toast: Ref<Components.ToastMessage> = inject('toast')

const editModal = ref<Components.Modal>()
const deleteModal = ref<Components.Modal>()

const pagination = ref<Components.Pagination<User>>()

const users = ref<User[]>([])
const roles = ref<UserRole[]>([])

const currentUser = ref<User>()
const currentRole = ref<Role>()

const isDifferentRole = computed(() => currentRole.value == currentUser.value.role)

const onEdit = (user: User) => {
    currentUser.value = user
    currentRole.value = user.role
    editModal.value.show()
}

const onConfirmEdit = async () => {
    await updateUser(currentUser.value.id, {
        userRole: currentRole.value
    }).then(_ => {
        currentUser.value.role = currentRole.value
        editModal.value.hide()
    }).catch((err) => {
        toast.value.show(err.response.data.detail, 'error', 4000)
    })    
}

const onDelete = async (user: User) => {
    currentUser.value = user
    deleteModal.value.show()
}

const onConfirmDelete = async () => {
    await deleteUser(currentUser.value.id)
        .then(_ => {
            users.value = users.value.filter(u => u.id != currentUser.value.id)
        }).catch((err) => {
            toast.value.show(err.response.data.detail, 'error', 4000)
        })
}

onMounted(async () => {
    users.value = await getUsers()
    roles.value = getAllRoles()
})
</script>